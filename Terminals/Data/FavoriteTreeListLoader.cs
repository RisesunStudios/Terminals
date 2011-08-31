﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Terminals.Configuration;
using Terminals.Data;
using Terminals.Forms.Controls;

namespace Terminals
{
    /// <summary>
    /// Fills tree list with favorites
    /// </summary>
    internal class FavoriteTreeListLoader
    {
        private TreeView treeList;
        private const String UNTAGGED_NODENAME = "Untagged";

        internal FavoriteTreeListLoader(TreeView treeListToFill)
        {
            this.treeList = treeListToFill;
            DataDispatcher.Instance.TagsChanged += new TagsChangedEventHandler(this.OnTagsCollectionChanged);
            DataDispatcher.Instance.FavoritesChanged += new FavoritesChangedEventHandler(this.OnFavoritesCollectionChanged);
        }

        /// <summary>
        /// Unregisters the Data dispatcher eventing.
        /// Call this to release the treeview, otherwise it will result in memory gap.
        /// </summary>
        internal void UnregisterEvents()
        {
            DataDispatcher.Instance.TagsChanged -= new TagsChangedEventHandler(this.OnTagsCollectionChanged);
            DataDispatcher.Instance.FavoritesChanged -= new FavoritesChangedEventHandler(this.OnFavoritesCollectionChanged);
        }

        private void OnFavoritesCollectionChanged(FavoritesChangedEventArgs args)
        {
            if(IsOrphan())
              return;

            RemoveFavorites(args.Removed);
            UpdateFavorites(args.Updated);
            AddNewFavorites(args.Added);
        }

        /// <summary>
        /// This prevents performance problems, when someone forgets to unregister.
        /// Returns true, if the associated treeview is already dead; otherwise false.
        /// </summary>
        private Boolean IsOrphan()
        {
            if (this.treeList.IsDisposed)
            {
                this.UnregisterEvents();
                return true;
            }

            return false;
        }

        private void RemoveFavorites(List<FavoriteConfigurationElement> removedFavorites)
        {
            foreach (FavoriteConfigurationElement favorite in removedFavorites) // remove
            {
                foreach (String tag in favorite.TagList)
                {
                    TagTreeNode tagNode = this.treeList.Nodes[tag] as TagTreeNode;
                    this.RemoveFavoriteFromTagNode(tagNode, favorite.Name);
                }
            }
        }

        private void UpdateFavorites(Dictionary<String, FavoriteConfigurationElement> updatedFavorites)
        {
            foreach (var updateArg in updatedFavorites)
            {
                foreach (TagTreeNode tagNode in this.treeList.Nodes)
                {
                    this.RemoveFavoriteFromTagNode(tagNode, updateArg.Key);  
                }

                this.AddFavoriteToAllItsTagNodes(updateArg.Value);
            }
        }

        private void AddNewFavorites(List<FavoriteConfigurationElement> addedFavorites)
        {
            foreach (FavoriteConfigurationElement favorite in addedFavorites)
            {
                this.AddFavoriteToAllItsTagNodes(favorite);
            }
        }

        private void AddFavoriteToAllItsTagNodes(FavoriteConfigurationElement favorite)
        {
            foreach (String tag in favorite.TagList)
            {
                TagTreeNode tagNode = this.treeList.Nodes[tag] as TagTreeNode;
                if (tagNode != null && !tagNode.IsEmpty)
                {
                    this.AddNewFavoriteNodeToTagNode(favorite, tagNode);
                }
            }
        }

        private void RemoveFavoriteFromTagNode(TagTreeNode tagNode, String favoriteName)
        {
            if (tagNode != null && !tagNode.IsEmpty)
                tagNode.Nodes.RemoveByKey(favoriteName);
        }

        private void AddNewFavoriteNodeToTagNode(FavoriteConfigurationElement favorite, TagTreeNode tagNode)
        {
            var favoriteTreeNode = new FavoriteTreeNode(favorite);
            tagNode.Nodes.Add(favoriteTreeNode);
        }

        private void OnTagsCollectionChanged(TagsChangedArgs args)
        {
            if (IsOrphan())
                return;

            RemoveUnusedTagNodes(args.Removed);
            AddMissingTagNodes(args.Added);
        }

        private void AddMissingTagNodes(List<String> newTags)
        {
            foreach (String newTag in newTags)
            {
                this.CreateAndAddTagNode(newTag);
            }
        }

        private void RemoveUnusedTagNodes(List<String> removedTags)
        {
            foreach (String obsoletTag in removedTags)
            {
                this.treeList.Nodes.RemoveByKey(obsoletTag);
            }
        }

        internal void Load()
        {

        }

        private void CreateAndAddTagNode(String tag)
        {
            TagTreeNode tagNode = new TagTreeNode(tag);
            treeList.Nodes.Add(tagNode);
        }

        internal void LoadTags()
        {
            this.CreateAndAddTagNode(UNTAGGED_NODENAME);

            foreach (string tagName in Settings.Tags)
            {
                this.CreateAndAddTagNode(tagName);
            }
        }

        internal void LoadFavorites(TagTreeNode tagNode)
        {
            if (tagNode.IsEmpty)
            {
                tagNode.Nodes.Clear();
                AddFavoriteNodes(tagNode);
            }
        }

        private void AddFavoriteNodes(TagTreeNode tagNode)
        {
            List<FavoriteConfigurationElement> tagFavorites = GetFavoritesByTag(tagNode.Text);
            foreach (var favorite in tagFavorites)
            {
                var favoriteTreeNode = new FavoriteTreeNode(favorite);
                tagNode.Nodes.Add(favoriteTreeNode);
            }
        }

        private static List<FavoriteConfigurationElement> GetFavoritesByTag(String tag)
        {
            return Settings.GetFavorites().ToList()
                .Where(favorite => favorite.TagList.Contains(tag, StringComparer.CurrentCultureIgnoreCase))
                .ToList();
        }
    }
}
