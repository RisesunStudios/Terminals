﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Terminals.Data.DB
{
    internal partial class Group : IGroup
    {
        private Guid guid = Guid.NewGuid();

        /// <summary>
        /// Gets the uniqeu identifier of this goup.
        /// This property is redundant and used only for internal use to reduce interface type casting.
        /// It isnt persisted. See database Id property.
        /// </summary>
        internal Guid Guid
        {
            get { return this.guid; }
        }

        Guid IGroup.Id
        {
            get { return this.guid; }
        }

        /// <summary>
        /// Gets or sets the virtual unique identifer. This isnt used, because of internal database identifier.
        /// Only for compatibility with file persistance.
        /// </summary>
        public Guid Parent
        {
            get
            {
                return this.ParentGroup != null ? this.ParentGroup.Guid : Guid.Empty;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        List<IFavorite> IGroup.Favorites
        {
            get { return this.Favorites.Cast<IFavorite>().ToList(); }
        }

        public void AddFavorite(IFavorite favorite)
        {
            AddFavoriteToDatabase(favorite);
            Data.Group.ReportGroupChanged(this);
        }

        private void AddFavoriteToDatabase(IFavorite favorite)
        {
            this.Favorites.Add((Favorite)favorite);
        }

        public void AddFavorites(List<IFavorite> favorites)
        {
            AddFavoritesToDatabase(favorites);
            Data.Group.ReportGroupChanged(this);
        }

        private void AddFavoritesToDatabase(List<IFavorite> favorites)
        {
            foreach (IFavorite favorite in favorites)
            {
                AddFavoriteToDatabase(favorite);
            }
        }

        public void RemoveFavorite(IFavorite favorite)
        {
            RemoveFavoriteFromDatabase(favorite);
            Data.Group.ReportGroupChanged(this);
        }

        public void RemoveFavorites(List<IFavorite> favorites)
        {
            RemoveFavoritesFromDatabase(favorites);
            Data.Group.ReportGroupChanged(this);
        }

        private void RemoveFavoritesFromDatabase(List<IFavorite> favorites)
        {
            foreach (IFavorite favorite in favorites)
            {
                RemoveFavoriteFromDatabase(favorite);
            }
        }

        private void RemoveFavoriteFromDatabase(IFavorite favorite)
        {
            this.Favorites.Remove((Favorite)favorite);
        }

        public override bool Equals(object group)
        {
            Group oponent = group as Group;
            if (oponent == null)
                return false;

            return this.Id.Equals(oponent.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return Data.Group.ToString(this);
        }
    }
}
