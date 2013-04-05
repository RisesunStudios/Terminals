﻿using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Terminals.Configuration;
using Terminals.Data;
using Terminals.Security;
using Terminals.Updates;

namespace Tests.Passwords
{
    /// <summary>
    /// Tests to ensure that passwords after upgrade of v2.0 work
    /// and upgrade doesn't break any stored passwords
    /// </summary>
    [TestClass]
    public class PasswordUpgradesTests
    {
        private const string TESTDATA_DIRECTORY = @"..\Resources\TestData\";
        private const string EMPTY_CONFIG_FILE = "EmptyTerminals.config";
        private const string EMPTY_CREDENTIALS_FILE = "EmptyCredentials.xml";
        private const string NOMASTER_CONFIG_FILE = "NoMasterTerminals.config";
        private const string NOMASTER_CREDENTIALS_FILE = "NoMasterCredentials.xml";
        private const string SECURED_CONFIG_FILE = "SecuredTerminals.config";
        private const string SECURED_CREDENTIALS_FILE = "SecuredCredentials.xml";

        /// <summary>
        /// User name and password encrypted in test credential file
        /// </summary>
        private const string TEST_PASSWORD = "TestUser";
        
        public TestContext TestContext { get; set; }

        private bool askedForPassword;

        [DeploymentItem(TESTDATA_DIRECTORY + EMPTY_CONFIG_FILE)]
        [DeploymentItem(TESTDATA_DIRECTORY + EMPTY_CREDENTIALS_FILE)]
        [TestMethod]
        public void V2UpgradeEmptyConfigTest()
        {
            this.UpgradePasswordsTestInitialize(EMPTY_CONFIG_FILE, EMPTY_CREDENTIALS_FILE);
            // simply nothing to upgrade, procedure shouldn't fail.
            this.RunUpgrade();
            Assert.IsFalse(askedForPassword, "Empty config file shouldn't ask for password");
        }

        [DeploymentItem(TESTDATA_DIRECTORY + NOMASTER_CONFIG_FILE)]
        [DeploymentItem(TESTDATA_DIRECTORY + NOMASTER_CREDENTIALS_FILE)]
        [TestMethod]
        public void V2UpgradeNoMasterPasswordConfigTest()
        {
            this.UpgradePasswordsTestInitialize(NOMASTER_CONFIG_FILE, NOMASTER_CREDENTIALS_FILE);
            // simply nothing to upgrade, procedure shouldn't fail.
            this.RunUpgrade();
            Assert.IsFalse(askedForPassword, "Config file shouldn't ask for password");
        }

        [DeploymentItem(TESTDATA_DIRECTORY + SECURED_CONFIG_FILE)]
        [DeploymentItem(TESTDATA_DIRECTORY + SECURED_CREDENTIALS_FILE)]
        [TestMethod]
        public void V2UpgradePasswordsTest()
        {
            this.UpgradePasswordsTestInitialize(SECURED_CONFIG_FILE, SECURED_CREDENTIALS_FILE);
            IPersistence persistence = this.RunUpgrade();

            bool masterStillValid = PasswordFunctions2.MasterPasswordIsValid(PasswordTests.MASTERPASSWORD, Settings.MasterPasswordHash);
            Assert.IsTrue(masterStillValid, "Master password upgrade failed.");

            // we don't have to authenticate, because it was already done by upgrade
            IFavorite favorite = persistence.Favorites.First();
            String favoritePassword = favorite.Security.Password;
            Assert.AreEqual(PasswordTests.USERPASSWORD, favoritePassword, "Upgrade favorite password failed.");

            ICredentialSet credential = persistence.Credentials.First();
            Assert.AreEqual(TEST_PASSWORD, credential.UserName, "Credential user name upgrade failed.");
            Assert.AreEqual(TEST_PASSWORD, credential.Password, "Credential password upgrade failed.");
        }

        private IPersistence RunUpgrade()
        {
            var persistence = new FilePersistence();
            var contentUpgrade = new FilesV2ContentUpgrade(persistence, GetMasterPassword);
            contentUpgrade.Run();
            Settings.ForceReload(); // because we changed its file, while upgrading
            return persistence;
        }

        private AuthenticationPrompt GetMasterPassword(bool retry)
        {
            Assert.IsFalse(askedForPassword, "Upgrade asks for password second time");
            askedForPassword = true;
            // simulate user prompt for master password
            return new AuthenticationPrompt { Password = PasswordTests.MASTERPASSWORD };
        }

        private void UpgradePasswordsTestInitialize(string configFile, string credentialsFile)
        {
            string configFileName = this.CreateFullTestFileName(configFile);
            string favoritesFileName = this.CreateFullTestFileName(FileLocations.FAVORITES_FILENAME);
            string credentialsFileName = this.CreateFullTestFileName(credentialsFile);
            // remove source control read only attribute
            File.SetAttributes(configFileName, FileAttributes.Normal);
            File.SetAttributes(credentialsFileName, FileAttributes.Normal);

            // we have to force all values to test deployment directory,
            // because upgrade works with fully configured files structure
            Settings.FileLocations.AssignCustomFileLocations(configFileName, favoritesFileName, credentialsFileName);
            // when running multiple tests, there is may be already old configuration
            Settings.ForceReload();
        }

        private string CreateFullTestFileName(string fileName)
        {
            string deplymentDir = this.TestContext.DeploymentDirectory;
            return Path.Combine(deplymentDir, fileName);
        }
    }
}
