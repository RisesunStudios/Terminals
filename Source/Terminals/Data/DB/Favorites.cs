﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;

namespace Terminals.Data.DB
{
    /// <summary>
    /// SQL persisted favorites container
    /// </summary>
    internal class Favorites : IFavorites
    {
        private readonly Groups groups;

        private readonly StoredCredentials credentials;

        private readonly DataDispatcher dispatcher;
        private readonly EntitiesCache<Favorite> cache = new EntitiesCache<Favorite>();

        internal List<Favorite> Cached
        {
            get { return this.cache.ToList(); }
        }

        private bool isLoaded;

        private PersistenceSecurity persistenceSecurity;

        internal Favorites(Groups groups, StoredCredentials credentials,
            PersistenceSecurity persistenceSecurity, DataDispatcher dispatcher)
        {
            this.groups = groups;
            this.credentials = credentials;
            this.persistenceSecurity = persistenceSecurity;
            this.dispatcher = dispatcher;
        }

        IFavorite IFavorites.this[Guid favoriteId]
        {
            get
            {
                this.EnsureCache();
                return this.cache.FirstOrDefault(favorite => favorite.Guid == favoriteId);
            }
        }

        IFavorite IFavorites.this[string favoriteName]
        {
            get
            {
                this.EnsureCache();
                return this.cache.FirstOrDefault(favorite => 
                            favorite.Name.Equals(favoriteName, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        public void Add(IFavorite favorite)
        {
            var favoritesToAdd = new List<IFavorite> { favorite };
            this.Add(favoritesToAdd);
        }

        public void Add(List<IFavorite> favorites)
        {
            using (var database = Database.CreateInstance())
            {
                List<Favorite> toAdd = favorites.Cast<Favorite>().ToList();
                AddAllToDatabase(database, toAdd);
                database.SaveImmediatelyIfRequested();
                database.DetachAll(toAdd);
                this.cache.Add(toAdd);
                this.dispatcher.ReportFavoritesAdded(favorites);
            }
        }

        private void AddAllToDatabase(Database database, IEnumerable<Favorite> favorites)
        {
            foreach (Favorite favorite in favorites)
            {
                database.Favorites.AddObject(favorite);
            }
        }

        public void Update(IFavorite favorite)
        {
            using (var database = Database.CreateInstance())
            {
                var toUpdate = favorite as Favorite;
                if (toUpdate != null)
                {
                    database.Attach(toUpdate);
                    this.TrySaveAndReportFavoriteUpdate(toUpdate, database);
                }
            }
        }

        public void UpdateFavorite(IFavorite favorite, List<IGroup> newGroups)
        {
            using (var database = Database.CreateInstance())
            {
                var toUpdate = favorite as Favorite;
                if (toUpdate == null)
                    return;

                database.Attach(toUpdate);
                List<IGroup> addedGroups = this.groups.AddToDatabase(database, newGroups);
                // commit newly created groups, otherwise we cant add into them
                database.SaveImmediatelyIfRequested(); 
                List<Group> removedGroups = this.UpdateGroupsMembership(favorite, newGroups, database);
                database.SaveImmediatelyIfRequested();

                List<IGroup> removedToReport = this.groups.DeleteFromCache(removedGroups);
                this.dispatcher.ReportGroupsRecreated(addedGroups, removedToReport);
                this.TrySaveAndReportFavoriteUpdate(toUpdate, database);
            }
        }

        private List<Group> UpdateGroupsMembership(IFavorite favorite, List<IGroup> newGroups, Database database)
        {
            List<IGroup> redundantGroups = ListsHelper.GetMissingSourcesInTarget(favorite.Groups, newGroups);
            List<IGroup> missingGroups = ListsHelper.GetMissingSourcesInTarget(newGroups, favorite.Groups);
            Data.Favorites.AddIntoMissingGroups(favorite, missingGroups);
            Data.Groups.RemoveFavoritesFromGroups(new List<IFavorite> {favorite}, redundantGroups);
            List<Group> removedGroups = this.groups.DeleteEmptyGroupsFromDatabase(database);
            return removedGroups;
        }

        private void TrySaveAndReportFavoriteUpdate(Favorite toUpdate, Database database)
        {
            try
            {
                this.SaveAndReportFavoriteUpdated(database, toUpdate);
            }
            catch (OptimisticConcurrencyException)
            {
                this.TryToRefreshUpdatedFavorite(toUpdate, database);
            }
        }

        private void TryToRefreshUpdatedFavorite(Favorite toUpdate, Database database)
        {
            try
            {
                database.Refresh(RefreshMode.ClientWins, toUpdate);
                this.SaveAndReportFavoriteUpdated(database, toUpdate);
            }
            catch (InvalidOperationException)
            {
                this.cache.Delete(toUpdate);
                this.dispatcher.ReportFavoriteDeleted(toUpdate);
            }
        }

        private void SaveAndReportFavoriteUpdated(Database database, Favorite favorite)
        {
            favorite.MarkAsModified(database);
            database.SaveImmediatelyIfRequested();
            database.DetachFavorite(favorite);
            this.cache.Update(favorite);
            this.dispatcher.ReportFavoriteUpdated(favorite);
        }

        public void Delete(IFavorite favorite)
        {
            var favoritesToDelete = new List<IFavorite> { favorite };
            Delete(favoritesToDelete);
        }

        public void Delete(List<IFavorite> favorites)
        {
            using (var database = Database.CreateInstance())
            {
                List<Favorite> favoritesToDelete = favorites.Cast<Favorite>().ToList();
                DeleteFavoritesFromDatabase(database, favoritesToDelete);
                database.SaveImmediatelyIfRequested();
                this.groups.RefreshCache();
                List<Group> deletedGroups = this.groups.DeleteEmptyGroupsFromDatabase(database);
                database.SaveImmediatelyIfRequested();
                List<IGroup> groupsToReport = this.groups.DeleteFromCache(deletedGroups);
                this.dispatcher.ReportGroupsDeleted(groupsToReport);
                this.cache.Delete(favoritesToDelete);
                this.dispatcher.ReportFavoritesDeleted(favorites);
            }
        }

        private void DeleteFavoritesFromDatabase(Database database, List<Favorite> favorites)
        {
            // we dont have to attache the details, because they will be deleted by reference constraints
            database.AttachAll(favorites);
            DeleteAllFromDatabase(database, favorites);
        }

        private void DeleteAllFromDatabase(Database database, IEnumerable<Favorite> favorites)
        {
            foreach (Favorite favorite in favorites)
            {
                database.Favorites.DeleteObject(favorite);
            }
        }

        public SortableList<IFavorite> ToListOrderedByDefaultSorting()
        {
            return Data.Favorites.OrderByDefaultSorting(this);
        }

        public void ApplyCredentialsToAllFavorites(List<IFavorite> selectedFavorites, ICredentialSet credential)
        {
            using (var database = Database.CreateInstance())
            {
                Data.Favorites.ApplyCredentialsToFavorites(selectedFavorites, credential);
                SaveAndReportFavoritesUpdated(database, selectedFavorites);
            }
        }

        private void SaveAndReportFavoritesUpdated(Database database, List<IFavorite> selectedFavorites)
        {
            database.SaveImmediatelyIfRequested();
            List<Favorite> toUpdate = selectedFavorites.Cast<Favorite>().ToList();
            this.cache.Update(toUpdate);
            this.dispatcher.ReportFavoritesUpdated(selectedFavorites);
        }

        public void SetPasswordToAllFavorites(List<IFavorite> selectedFavorites, string newPassword)
        {
            using (var database = Database.CreateInstance())
            {
                Data.Favorites.SetPasswordToFavorites(selectedFavorites, newPassword);
                SaveAndReportFavoritesUpdated(database, selectedFavorites);
            }
        }

        public void ApplyDomainNameToAllFavorites(List<IFavorite> selectedFavorites, string newDomainName)
        {
            using (var database = Database.CreateInstance())
            {
                Data.Favorites.ApplyDomainNameToFavorites(selectedFavorites, newDomainName);
                SaveAndReportFavoritesUpdated(database, selectedFavorites);
            }
        }

        public void ApplyUserNameToAllFavorites(List<IFavorite> selectedFavorites, string newUserName)
        {
            using (var database = Database.CreateInstance())
            {
                Data.Favorites.ApplyUserNameToFavorites(selectedFavorites, newUserName);
                SaveAndReportFavoritesUpdated(database, selectedFavorites);
            }
        }

        private void EnsureCache()
        {
            if (isLoaded)
                return;
            
            List<Favorite> loaded = LoadFromDatabase();
            this.cache.Add(loaded);
            this.isLoaded = true;
        }

        internal void RefreshCache()
        {
            List<Favorite> newlyLoaded = LoadFromDatabase(this.Cached);
            List<Favorite> oldFavorites = this.Cached;
            List<Favorite> missing = ListsHelper.GetMissingSourcesInTarget(newlyLoaded, oldFavorites);
            List<Favorite> redundant = ListsHelper.GetMissingSourcesInTarget(oldFavorites, newlyLoaded);
            List<Favorite> toUpdate = ListsHelper.GetMissingSourcesInTarget(oldFavorites, redundant);

            this.cache.Add(missing);
            this.cache.Delete(redundant);
            this.RefreshCachedItems();

            var missingToReport = missing.Cast<IFavorite>().ToList();
            var redundantToReport = redundant.Cast<IFavorite>().ToList();
            var updatedToReport = toUpdate.Cast<IFavorite>().ToList();
            
            this.dispatcher.ReportFavoritesAdded(missingToReport);
            this.dispatcher.ReportFavoritesDeleted(redundantToReport);
            this.dispatcher.ReportFavoritesUpdated(updatedToReport);
        }

        private void RefreshCachedItems()
        {
            foreach (Favorite favorite in this.cache)
            {
                favorite.ReleaseLoadedDetails();
            }
        }

        private List<Favorite> LoadFromDatabase()
        {
            using (var database = Database.CreateInstance())
            {
                // to list because Linq to entities allowes only cast to primitive types
                List<Favorite> favorites = database.Favorites.ToList();
                database.DetachAll(favorites);
                favorites.ForEach(candidate => candidate.AssignStores(this.groups, this.credentials, this.persistenceSecurity));
                return favorites;
            }
        }

        private static List<Favorite> LoadFromDatabase(List<Favorite> toRefresh)
        {
            using (var database = Database.CreateInstance())
            {
                if (toRefresh != null)
                    database.AttachAll(toRefresh);

                // to list because Linq to entities allowes only cast to primitive types
                database.Refresh(RefreshMode.StoreWins, database.Favorites);
                List<Favorite> favorites = database.Favorites.ToList();
                database.DetachAll(favorites);
                return favorites;
            }
        }

        #region IEnumerable members

        public IEnumerator<IFavorite> GetEnumerator()
        {
            this.EnsureCache();
            return this.cache.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public override string ToString()
        {
            return string.Format("Favorites:Cached={0}", this.cache.Count());
        }
    }
}