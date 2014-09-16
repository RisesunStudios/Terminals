﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Terminals.Configuration;
using Terminals.Data;
using Tests.FilePersisted;

namespace Tests
{
    /// <summary>
    /// Till the Settings is static class we have not chance to isolate the other unit test.
    /// So we have to reset all settings possibly conflicting with other unit tests.
    /// </summary>
    [TestClass]
    public class ConfigurationSettingsTests
    {
        private static readonly Guid FavoriteA = new Guid("eb45aefd-138f-49ca-9c9e-ce5d5877ca62");
        private static readonly Guid FavoriteB = new Guid("8dda2ccb-cfb9-4487-bab5-0da5319e1485");

        [ClassInitialize]
        public static void ClassSetUp(TestContext context)
        {
            FilePersistedTestLab.SetDefaultFileLocations();
        }

        [TestMethod]
        public void FileWatch_FileChanged_FiresConfigurationChanged()
        {
            var originalWatch = Settings.FileWatch;
            var mockFileWatch = new Mock<IDataFileWatcher>(MockBehavior.Strict);
            Settings.FileWatch = mockFileWatch.Object;
            bool eventReceived = false;
            Settings.ConfigurationChanged += args => eventReceived = true;
            mockFileWatch.Raise(watch => watch.FileChanged += null, EventArgs.Empty);
            Assert.IsTrue(eventReceived, "When receiving file changed event, Settings have to fire configuration changed");
            Settings.FileWatch = originalWatch;
        }

        [TestMethod]
        public void DumyControl_AssignSynchronizer_AssignesToFileWatch()
        {
            var originalWatch = Settings.FileWatch;
            var expectedSynchronizer = new Control();
            var mockFileWatch = new Mock<IDataFileWatcher>(MockBehavior.Strict);
            mockFileWatch.Setup(watch => watch.AssignSynchronizer(expectedSynchronizer))
                         .Verifiable("The assigned synchronized wasnt delivered to the file watching service");
            Settings.FileWatch = mockFileWatch.Object;
            Settings.AssignSynchronizationObject(expectedSynchronizer);
            mockFileWatch.VerifyAll();
            Settings.FileWatch = originalWatch;
        }

        [TestMethod]
        public void DefaultSettings_Forms_DoesntReturnNull()
        {
            Assert.IsNotNull(Settings.Forms, "The forms configuration always has to be accessible");
        }

        [TestMethod]
        public void EmptyCaptureRoot_SetGet_ReturnsDefaultRoot()
        {
            Settings.CaptureRoot = string.Empty;
            const string MESSAGE = "Undefined capture root has to return default location";
            Assert.AreEqual(FileLocations.DefaultCaptureRootDirectory, Settings.CaptureRoot, MESSAGE);
        }

        [TestMethod]
        public void ProtocolDefaultSortProperty_SetGet_IsSaved()
        {
            Settings.DefaultSortProperty = SortProperties.Protocol;
            const string MESSAGE = "Get DefaultSortProperty has to return last saved value";
            Assert.AreEqual(SortProperties.Protocol, Settings.DefaultSortProperty, MESSAGE);
            Settings.DefaultSortProperty = SortProperties.ConnectionName;
        }

        [TestMethod]
        public void Vesion2_SetGet_IsSaved()
        {
            Version expectedVersion = new Version(2, 0);
            Settings.ConfigVersion = expectedVersion;
            Assert.AreEqual(expectedVersion, Settings.ConfigVersion, "Get config file version has to return last saved value");
        }

        [TestMethod]
        public void TwoConnections_CreateSavedConnectionsList_AreSaved()
        {
            const string CONNECTION_B = "CONNECTION_B";
            var connecitonNames = new string[] {"FavoriteA", CONNECTION_B};
            Settings.CreateSavedConnectionsList(connecitonNames);
            var saved = Settings.SavedConnections;
            Assert.AreEqual(CONNECTION_B, saved[1], "Saved connections should correspond with last saved connections list.");
        }

        [TestMethod]
        public void FavoriteB_EditFavoriteButton_IsSaved()
        {
            Settings.UpdateFavoritesToolbarButtons(new List<Guid>());
            Settings.EditFavoriteButton(FavoriteA, FavoriteB, true);
            var savedButtons = Settings.FavoritesToolbarButtons;
            Assert.AreEqual(FavoriteB, savedButtons[0], "Loaded buttons should contain last saved buttons.");
        }

        [TestMethod]
        public void TwoFavorites_UpdateButtons_FiresSettingsChanged()
        {
            var favorites = new List<Guid>() { FavoriteA, FavoriteB };
            bool changeReported = false;
            Settings.ConfigurationChanged += args => changeReported = true;
            Settings.UpdateFavoritesToolbarButtons(favorites);
            Assert.IsTrue(changeReported, "Changing buttons has to fire event to report the imediate change");
        }

        [TestMethod]
        public void SavedValue_ForceReload_IsLoaded()
        {
            this.AssertAskToReconnectReloaded(false, "Direct assignment of property saves the property to file");
        }

        [TestMethod]
        public void DelayedSave_ForceReload_LosesValue()
        {
            Settings.StartDelayedUpdate();
            const string MESSAGE = "When delayed save was called, directly changing settings doesnt force save.";
            this.AssertAskToReconnectReloaded(true, MESSAGE);
            Settings.SaveAndFinishDelayedUpdate();
        }

        private void AssertAskToReconnectReloaded(bool expectedValue, string message)
        {
            Settings.AskToReconnect = false; // because default is true
            Settings.ForceReload();
            Assert.AreEqual(expectedValue, Settings.AskToReconnect, message);
            Settings.AskToReconnect = true;
        }
    }
}
