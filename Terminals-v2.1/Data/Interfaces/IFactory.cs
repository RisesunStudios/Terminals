﻿using System;
using System.Collections.Generic;

namespace Terminals.Data
{
    /// <summary>
    /// Provides access to create persisted item from one place
    /// </summary>
    internal interface IFactory
    {
        /// <summary>
        /// Creates new, not configured instance of connection favorite. Does not add it to the persistance.
        /// </summary>
        /// <returns>Not null newly created instance</returns>
        IFavorite CreateFavorite();

        /// <summary>
        /// Creates new empty, not configured group. Does not add it to the persistance.
        /// </summary>
        /// <param name="groupName">New name to assign</param>
        /// <param name="favorites">The favorites collection to be assigned to the group.</param>
        /// <returns>
        /// Not null, newly created group
        /// </returns>
        IGroup CreateGroup(string groupName, List<IFavorite> favorites = null); 
       
        /// <summary>
        /// Creates new empty credentials item. Does not add it to the persistance.
        /// </summary>
        /// <returns>Not null newly created instance</returns>
        ICredentialSet CreateCredentialSet();
    }
}
