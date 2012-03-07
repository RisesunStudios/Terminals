﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Terminals.Configuration;
using SysConfig = System.Configuration;
using System.IO;
using System.Linq;
using Unified;

namespace Terminals.Data
{
    internal sealed class StoredCredentials: ICredentials
    {
        private List<ICredentialSet> cache;

        private Mutex fileLock = new Mutex(false, "Terminals.CodePlex.com.Credentials");
        private DataFileWatcher fileWatcher;

        /// <summary>
        /// Prevents creating from other class
        /// </summary>
        internal StoredCredentials()
        {
            this.cache = new List<ICredentialSet>();
            string configFileName = Settings.FileLocations.Credentials;
            InitializeFileWatch();

            if (File.Exists(configFileName))
                LoadStoredCredentials(configFileName);
            else
                Save();
        }

        private void InitializeFileWatch()
        {
            fileWatcher = new DataFileWatcher(Settings.FileLocations.Credentials);
            fileWatcher.FileChanged += new EventHandler(CredentialsFileChanged);
            fileWatcher.StartObservation();
        }

        private void CredentialsFileChanged(object sender, EventArgs e)
        {
            LoadStoredCredentials(Settings.FileLocations.Credentials);
            if (CredentialsChanged != null)
                CredentialsChanged(this, new EventArgs());
        }

        internal void AssignSynchronizationObject(ISynchronizeInvoke synchronizer)
        {
            fileWatcher.AssignSynchronizer(synchronizer);
        }

        private void LoadStoredCredentials(string configFileName)
        {
            List<ICredentialSet> loaded = LoadFile(configFileName);
            if (loaded != null)
            {
                this.cache.Clear();
                this.cache.AddRange(loaded);
            }
        }

        private List<ICredentialSet> LoadFile(string configFileName)
        {
            try
            {
                fileLock.WaitOne();
                object loadedObj = Serialize.DeserializeXMLFromDisk(configFileName, typeof(List<CredentialSet>));
                return (loadedObj as List<CredentialSet>)
                    .Cast<ICredentialSet>().ToList();
            }
            catch (Exception exception)
            {
                string errorMessage = String.Format("Load credentials from {0} failed.", configFileName);
                Logging.Log.Error(errorMessage, exception);
                return new List<ICredentialSet>();
            }
            finally
            {
                fileLock.ReleaseMutex();
                Debug.WriteLine("Credentials file Loaded.");
            }
        }

        #region ICredentials

        public event EventHandler CredentialsChanged;

        public ICredentialSet this[Guid id]
        {
            get
            {
                return this.cache.FirstOrDefault(candidate => candidate.Id.Equals(id));
            }
        }

        /// <summary>
        /// Gets a credential by its name from cached credentials.
        /// This method isnt case sensitive. If no item matches, returns null.
        /// </summary>
        /// <param name="name">name of an item to search</param>
        public ICredentialSet this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                    return null;

                return this.cache.FirstOrDefault(candidate => candidate.Name
                   .Equals(name, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        public void Add(ICredentialSet toAdd)
        {
            if (String.IsNullOrEmpty(toAdd.Name))
                return;

            this.cache.Add(toAdd);
        }

        public void Remove(ICredentialSet toRemove)
        {
            cache.Remove(toRemove);
        }

        public void UpdatePasswordsByNewKeyMaterial(string newKeyMaterial)
        {
            foreach (ICredentialSet credentials in cache)
            {
                credentials.UpdatePasswordByNewKeyMaterial(newKeyMaterial);
            }

            Save();
        }

        public void Save()
        {
            try
            {
                this.fileLock.WaitOne();
                this.fileWatcher.StopObservation();
                string fileName = Settings.FileLocations.Credentials;
                List<CredentialSet> fileContent = this.cache.Cast<CredentialSet>().ToList();
                Serialize.SerializeXMLToDisk(fileContent, fileName);
                Debug.WriteLine("Credentials file saved.");
            }
            catch (Exception exception)
            {
                string errorMessage = string.Format("Save credentials to {0} failed.",
                    Settings.FileLocations.Credentials);
                Logging.Log.Error(errorMessage, exception);
            }
            finally
            {
                this.fileWatcher.StartObservation();
                this.fileLock.ReleaseMutex();
            }
        }

        #endregion

        #region IEnumerable members

        public IEnumerator<ICredentialSet> GetEnumerator()
        {
            return this.cache.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
