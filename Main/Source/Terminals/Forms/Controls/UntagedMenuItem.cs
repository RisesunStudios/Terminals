﻿using System.Collections.Generic;
using Terminals.Configuration;
using Terminals.Data;

namespace Terminals.Forms.Controls
{
    /// <summary>
    /// Virtual menu item used to collect favorites without assigned any group
    /// </summary>
    internal class UntagedMenuItem : GroupMenuItem
    {
        internal override List<IFavorite> Favorites
        {
            get
            {
                var favorites = Persistence.Instance.Favorites;
                return FavoriteTreeListLoader.GetUntaggedFavorites(favorites);
            }
        }

        internal UntagedMenuItem()
            : base(Settings.UNTAGGED_NODENAME, true)
        {
        }
    }
}
