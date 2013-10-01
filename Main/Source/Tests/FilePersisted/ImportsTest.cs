﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Terminals;
using Terminals.Data;
using Terminals.Forms.Controls;
using Terminals.Integration;
using Terminals.Integration.Export;
using Terminals.Integration.Import;

namespace Tests.FilePersisted
{
    [TestClass]
    [DeploymentItem(@"..\Resources\TestData\" + DUPLICIT_ITEMS_FILE)]
    public class ImportsTest : FilePersistedTestLab
    {
        private const string DUPLICIT_ITEMS_FILE = @"Issue_21279_import_800_items.xml";

        private const string TEST_FILE = "testOne.xml";

        private const string TEST_PASSWORD = "aaa";

        private const string TEST_DOMAIN = "testDomain";

        private const string TEST_USERNAME = "testUser";

        public TestContext TestContext { get; set; }

        // following functions simulate the answer usually provided by user in UI
        private static readonly Func<int, DialogResult> rename = itemsCount => DialogResult.Yes;
        private static readonly Func<int, DialogResult> overwrite = itemsCount => DialogResult.No;

        private int PersistenceFavoritesCount
        {
            get
            {
                return this.Persistence.Favorites.Select(favorite => favorite.Name)
                    .Distinct(StringComparer.CurrentCultureIgnoreCase)
                    .Count();
            }
        }

        private int ImportedGroupsCount
        {
            get
            {
                return this.Persistence.Groups.Select(favorite => favorite.Name)
                    .Distinct(StringComparer.CurrentCultureIgnoreCase)
                    .Count();
            }
        }

        [TestMethod]
        public void ExportImportFavoriteTest()
        {
            IPersistence persistence = this.Persistence;
            ExportImportFavorite(persistence, this.TestContext.DeploymentDirectory);
        }

        /// <summary>
        /// More regression test than unit test
        /// </summary>
        internal static void ExportImportFavorite(IPersistence persistence, string path)
        {
            IFavorite favorite = CreateTestFavorite(persistence);
            ExportFavorite(favorite, persistence);
            // to preserve test against identical favorite
            persistence.Favorites.Delete(favorite);
            List<FavoriteConfigurationElement> toImport = ImportItemsFromFile(path, TEST_FILE);
            // persisted favorites are empty, strategy doesnt matter
            InvokeTheImport(toImport, persistence, rename);
            var importedSecurity = persistence.Favorites.First().Security;
            Assert.AreEqual(TEST_USERNAME, importedSecurity.UserName);
            Assert.AreEqual(TEST_DOMAIN, importedSecurity.Domain);
            Assert.AreEqual(TEST_PASSWORD, importedSecurity.Password);
        }

        private static IFavorite CreateTestFavorite(IPersistence persistence)
        {
            IFavorite favorite = persistence.Factory.CreateFavorite();
            favorite.Name = "testFavorite";
            favorite.ServerName = favorite.Name;
            var security = favorite.Security;
            security.UserName = TEST_USERNAME;
            security.Domain = TEST_DOMAIN;
            security.Password = TEST_PASSWORD;
            persistence.Favorites.Add(favorite);
            return favorite;
        }

        private static void ExportFavorite(IFavorite favorite, IPersistence persistence)
        {
            FavoriteConfigurationElement favoriteElement = ModelConverterV2ToV1.ConvertToFavorite(favorite, persistence);

            ExportOptions options = new ExportOptions
                {
                    ProviderFilter = ImportTerminals.TERMINALS_FILEEXTENSION,
                    Favorites = new List<FavoriteConfigurationElement> {favoriteElement},
                    FileName = TEST_FILE,
                    IncludePasswords = true
                };
            Integrations.Exporters.Export(options);
        }

        /// <summary>
        /// Tries to import duplicate items into the file persistence renaming duplicate items
        ///</summary>
        [TestMethod]
        public void ImportRenamingDuplicitFavoritesTest()
        {
            this.ImportDuplicitFavoritesTest(rename, 2);
        }

        /// <summary>
        /// Tries to import duplicate items into the file persistence overwriting duplicate items
        ///</summary>
        [TestMethod]
        public void ImportOverwritingDuplicitFavoritesTest()
        {
            this.ImportDuplicitFavoritesTest(overwrite, 1);
        }

        private void ImportDuplicitFavoritesTest(Func<int, DialogResult> strategy, int expectedSecondImportCount)
        {
            // call import first to force the persistence initialization
            List<FavoriteConfigurationElement> toImport = ImportItemsFromFile(this.TestContext.DeploymentDirectory);

            // 887 obtained by manual check of the xml elements
            Assert.AreEqual(887, toImport.Count, "Some items from Import file were not identified");
            object result = InvokeTheImport(toImport, this.Persistence, strategy);
            Assert.AreEqual(true, result, "Import wasn't successful");
            int expected = ExpectedFavoritesCount(toImport);
            Assert.AreEqual(expected, this.PersistenceFavoritesCount, "Imported favorites count doesn't match.");
            InvokeTheImport(toImport, this.Persistence, strategy);
            Assert.AreEqual(expected * expectedSecondImportCount, this.PersistenceFavoritesCount,
                "Imported favorites count doesn't match after second import");
            int expectedGroups = this.Persistence.Groups.Count();
            Assert.AreEqual(expectedGroups, this.ImportedGroupsCount, "Imported groups count doesn't match.");
        }

        private static object InvokeTheImport(List<FavoriteConfigurationElement> toImport, IPersistence persistence,
            Func<int, DialogResult> strategy)
        {
            var managedImport = new ImportWithDialogs(null, persistence);
            var privateObject = new PrivateObject(managedImport);
            return privateObject.Invoke("ImportPreservingNames", new object[] { toImport, strategy });
        }

        private static int ExpectedFavoritesCount(List<FavoriteConfigurationElement> toImport)
        {
            return toImport.Select(favorite => favorite.Name)
                           .Distinct(StringComparer.CurrentCultureIgnoreCase)
                           .Count();
        }

        private static List<FavoriteConfigurationElement> ImportItemsFromFile(string path, 
            string fileName = DUPLICIT_ITEMS_FILE)
        {
            string fullFileName = Path.Combine(path, fileName);
            return Integrations.Importers.ImportFavorites(fullFileName);
        }
    }
}