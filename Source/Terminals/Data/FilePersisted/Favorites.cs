using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Terminals.Configuration;
using Terminals.Connections;
using Terminals.Data.Credentials;
using Terminals.Data.FilePersisted;

namespace Terminals.Data
{
    internal class Favorites : IFavorites
    {
        private readonly DataDispatcher dispatcher;
        private readonly FilePersistence persistence;

        private readonly FavoriteIcons favoriteIcons;

        private readonly ConnectionManager connectionManager;

        private readonly Groups groups;
        private readonly Dictionary<Guid, IFavorite> cache;

        private readonly FavoriteBatchUpdates batchUpdates;

        internal Favorites(FilePersistence persistence, FavoriteIcons favoriteIcons, ConnectionManager connectionManager)
        {
            this.persistence = persistence;
            this.favoriteIcons = favoriteIcons;
            this.connectionManager = connectionManager;
            this.dispatcher = persistence.Dispatcher;
            this.groups = this.persistence.GroupsStore;
            this.cache = new Dictionary<Guid,IFavorite>();
            this.batchUpdates = new FavoriteBatchUpdates(persistence);
        }

        private bool AddToCache(IFavorite favorite)
        {
            if (favorite == null || this.cache.ContainsKey(favorite.Id))
                return false;

            this.cache.Add(favorite.Id, favorite);
            return true;
        }

        internal List<IFavorite> AddAllToCache(List<IFavorite> favorites)
        {
            var added = new List<IFavorite>();
            var onlyKnownProtocols = this.FilterOnlyKnownProtocols(favorites);

            foreach (IFavorite favorite in onlyKnownProtocols)
            {
                if (this.AddToCache(favorite))
                    added.Add(favorite);
            }
            return added;
        }

        private IEnumerable<IFavorite> FilterOnlyKnownProtocols(List<IFavorite> favorites)
        {
            string[] availableProtocols = this.connectionManager.GetAvailableProtocols();
            return favorites.Where(f => availableProtocols.Contains(f.Protocol));
        }

        private bool DeleteFromCache(IFavorite favorite)
        {
            if (IsNotCached(favorite))
                return false;

            this.cache.Remove(favorite.Id);
            return true;
        }

        private List<IFavorite> DeleteAllFavoritesFromCache(List<IFavorite> favorites)
        {
            var deleted = new List<IFavorite>();
            foreach (IFavorite favorite in favorites)
            {
                if (this.DeleteFromCache(favorite))
                    deleted.Add(favorite);
            }
            return deleted;
        }

        private bool UpdateInCache(IFavorite favorite)
        {
            if (IsNotCached(favorite))
                return false;

            this.cache[favorite.Id] = favorite;
            return true;
        }

        private bool IsNotCached(IFavorite favorite)
        {
            return favorite == null || !this.cache.ContainsKey(favorite.Id);
        }

        internal void Merge(List<IFavorite> newFavorites)
        {
            var oldFavorites = new List<IFavorite>(this);
            List<IFavorite> missingFavorites = ListsHelper.GetMissingSourcesInTarget(newFavorites, oldFavorites);
            List<IFavorite> redundantFavorites = ListsHelper.GetMissingSourcesInTarget(oldFavorites, newFavorites);
            this.AddToCacheAndReport(missingFavorites);
            List<IFavorite> deleted = this.DeleteAllFavoritesFromCache(redundantFavorites);
            // dont remove favorites from groups, because we are expecting, that the loaded file already contains correct membership
            this.dispatcher.ReportFavoritesDeleted(deleted);
            // Simple update without ensuring, if the favorite was changes or not - possible performance issue);
            List<IFavorite> notReported = ListsHelper.GetMissingSourcesInTarget(this.ToList(), missingFavorites);
            this.dispatcher.ReportFavoritesUpdated(notReported);
        }

        internal static SortableList<IFavorite> OrderByDefaultSorting(IEnumerable<IFavorite> source)
        {
            IOrderedEnumerable<IFavorite> sorted;
            switch (Settings.Instance.DefaultSortProperty)
            {
                case SortProperties.ServerName:
                    sorted = source.OrderBy(favorite => favorite.ServerName)
                        .ThenBy(favorite => favorite.Name);
                    break;

                case SortProperties.Protocol:
                    sorted = source.OrderBy(favorite => favorite.Protocol)
                        .ThenBy(favorite => favorite.Name);
                    break;
                case SortProperties.ConnectionName:
                    sorted = source.OrderBy(favorite => favorite.Name);
                    break;
                default:
                    return new SortableList<IFavorite>(source);
            }

            return new SortableList<IFavorite>(sorted);
        }

        private void UpdateFavoriteInGroups(IFavorite favorite, List<IGroup> newGroups)
        {
            var oldGroups = this.groups.GetGroupsContainingFavorite(favorite.Id);
            List<IGroup> addedGroups = this.AddIntoMissingGroups(favorite, newGroups, oldGroups);
            List<IGroup> redundantGroups = ListsHelper.GetMissingSourcesInTarget(oldGroups, newGroups);
            Groups.RemoveFavoritesFromGroups(new List<IFavorite> { favorite }, redundantGroups);
            this.dispatcher.ReportGroupsAdded(addedGroups);
        }

        private List<IGroup> AddIntoMissingGroups(IFavorite favorite, List<IGroup> newGroups, List<IGroup> oldGroups)
        {
            // First create new groups, which aren't in persistence yet
            var addedGroups = this.groups.AddAllToCache(newGroups);
            List<IGroup> missingGroups = ListsHelper.GetMissingSourcesInTarget(newGroups, oldGroups);
            AddIntoMissingGroups(favorite, missingGroups);
            return addedGroups;
        }

        internal static void AddIntoMissingGroups(IFavorite favorite, List<IGroup> missingGroups)
        {
            foreach (IGroup group in missingGroups)
            {
                group.AddFavorite(favorite);
            }
        }

        internal void UpdatePasswordsByNewMasterPassword(string newKeyMaterial)
        {
            foreach (Favorite favorite in this)
            {
                var guarded = this.CreateGuardedSecurity(favorite.Security);
                guarded.UpdatePasswordByNewKeyMaterial(newKeyMaterial);
                this.UpdatePasswordsInProtocolProperties(favorite.ProtocolProperties, newKeyMaterial);
            }
        }

        private void UpdatePasswordsInProtocolProperties(ProtocolOptions protocolProperties, string newKeyMaterial)
        {
            var options = protocolProperties as IContainsCredentials;
            if (options != null)
            {
                SecurityOptions securityOptions = options.GetSecurity();
                var guarded = this.CreateGuardedSecurity(securityOptions);
                guarded.UpdatePasswordByNewKeyMaterial(newKeyMaterial);
            }
        }

        private GuardedSecurity CreateGuardedSecurity(SecurityOptions favoriteSecurity)
        {
            return new GuardedSecurity(this.persistence, favoriteSecurity);
        }

        #region IFavorites members

        public IFavorite this[Guid favoriteId]
        {
            get
            {
                if (this.cache.ContainsKey(favoriteId))
                    return this.cache[favoriteId];
                return null;
            }
        }

        public IFavorite this[string favoriteName]
        {
            get
            {
                return this.FirstOrDefault(favorite => favorite.Name
                        .Equals(favoriteName, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        public void Add(IFavorite favorite)
        {
            if (AddToCache(favorite))
            {
                this.dispatcher.ReportFavoriteAdded(favorite);
                this.persistence.SaveImmediatelyIfRequested();
            }
        }

        public void Add(List<IFavorite> favorites)
        {
            if (favorites == null)
                return;

            List<IFavorite> added = this.AddToCacheAndReport(favorites);
            if (added.Count > 0)
                this.persistence.SaveImmediatelyIfRequested();
        }

        private List<IFavorite> AddToCacheAndReport(List<IFavorite> favorites)
        {
            List<IFavorite> added = this.AddAllToCache(favorites);
            this.dispatcher.ReportFavoritesAdded(added);
            return added;
        }

        public void Update(IFavorite favorite)
        {
            if (!UpdateInCache(favorite))
                return;

            SaveAndReportFavoriteUpdate(favorite);
        }

        public void UpdateFavorite(IFavorite favorite, List<IGroup> newGroups)
        {
            if (!UpdateInCache(favorite))
                return;

            UpdateFavoriteInGroups(favorite, newGroups);
            SaveAndReportFavoriteUpdate(favorite);
        }

        private void SaveAndReportFavoriteUpdate(IFavorite favorite)
        {
            this.dispatcher.ReportFavoriteUpdated(favorite);
            this.persistence.SaveImmediatelyIfRequested();
        }

        public void Delete(IFavorite favorite)
        {
            if (DeleteFromCache(favorite))
            {
                var favoritesToRemove = new List<IFavorite> { favorite };
                this.groups.DeleteFavoritesFromAllGroups(favoritesToRemove);
                this.dispatcher.ReportFavoriteDeleted(favorite);
                this.persistence.SaveImmediatelyIfRequested();
            }
        }

        public void Delete(List<IFavorite> favorites)
        {
            if (favorites == null)
                return;

            this.DeleteFromCacheAndReport(favorites);
            this.persistence.SaveImmediatelyIfRequested();
        }

        private void DeleteFromCacheAndReport(List<IFavorite> favorites)
        {
            List<IFavorite> deleted = this.DeleteAllFavoritesFromCache(favorites);
            this.groups.DeleteFavoritesFromAllGroups(deleted);
            this.dispatcher.ReportFavoritesDeleted(deleted);
        }

        public SortableList<IFavorite> ToList()
        {
            return new SortableList<IFavorite>(this);
        }

        public SortableList<IFavorite> ToListOrderedByDefaultSorting()
        {
            return OrderByDefaultSorting(this);
        }

        public void ApplyCredentialsToAllFavorites(List<IFavorite> selectedFavorites, ICredentialSet credential)
        {
            ApplyCredentialsToFavorites(selectedFavorites, credential);
            SaveAndReportFavoritesUpdate(selectedFavorites);
        }

        private void SaveAndReportFavoritesUpdate(List<IFavorite> selectedFavorites)
        {
            this.dispatcher.ReportFavoritesUpdated(selectedFavorites);
            this.persistence.SaveImmediatelyIfRequested();
        }

        internal static void ApplyCredentialsToFavorites(List<IFavorite> selectedFavorites, ICredentialSet credential)
        {
            foreach (IFavorite favorite in selectedFavorites)
            {
                favorite.Security.Credential = credential.Id;
            }
        }

        public void SetPasswordToAllFavorites(List<IFavorite> selectedFavorites, string newPassword)
        {
            this.batchUpdates.SetPasswordToFavorites(selectedFavorites, newPassword);
            this.SaveAndReportFavoritesUpdate(selectedFavorites);
        }

        public void ApplyDomainNameToAllFavorites(List<IFavorite> selectedFavorites, string newDomainName)
        {
            this.batchUpdates.ApplyDomainNameToFavorites(selectedFavorites, newDomainName);
            this.SaveAndReportFavoritesUpdate(selectedFavorites);
        }

        public void ApplyUserNameToAllFavorites(List<IFavorite> selectedFavorites, string newUserName)
        {
            this.batchUpdates.ApplyUserNameToFavorites(selectedFavorites, newUserName);
            this.SaveAndReportFavoritesUpdate(selectedFavorites);
        }

        public void UpdateFavoriteIcon(IFavorite favorite, string imageFilePath)
        {
            var toUpdate = favorite as Favorite;
            toUpdate.ToolBarIconFile = imageFilePath;
        }

        public Image LoadFavoriteIcon(IFavorite favorite)
        {
            var toUpdate = favorite as Favorite;
            if (toUpdate.ToolBarIconImage == null)
                toUpdate.ToolBarIconImage = favoriteIcons.GetFavoriteIcon(toUpdate);
            return toUpdate.ToolBarIconImage;
        }

        #endregion

        #region IEnumerable members

        public IEnumerator<IFavorite> GetEnumerator()
        {
            return this.cache.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
