using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Terminals.Configuration;
using Tests.FilePersisted;

namespace Tests.Connections
{
    /// <summary>
    /// Some tests need Deployed plugins in tests directory.
    /// Because of dynamic loading of the plugins, it is necessary to compare by Type name, because types doenst equals.
    /// This is necessary, because for testing we cant inject the plugins into Connection manager.
    /// See Serializer and its connectionmanager field usage, see also setter of Favorite Protocol property.
    /// </summary>
    [DeploymentItem(VNC_PLUGIN, VNC_TARGET)]
    [DeploymentItem(ICA_PLUGIN, ICA_TARGET)]
    [DeploymentItem(TERMINAL_PLUGIN, TERMINAL_TARGET)]
    [DeploymentItem(VMRC_PLUGIN, VMRC_TARGET)]
    [DeploymentItem(WEB_PLUGIN, WEB_TARGET)]
    [DeploymentItem(RDP_PLUGIN, RDP_TARGET)]
    [TestClass]
    public class PluginBasedTests
    {
        internal const string VNC_PLUGIN = "Terminals.Plugins.Vnc.dll";

        internal const string VNC_TARGET = @"Plugins\Vnc";

        internal const string ICA_PLUGIN = "Terminals.Plugins.Ica.dll";

        internal const string ICA_TARGET = @"Plugins\Ica";

        internal const string TERMINAL_PLUGIN = "Terminals.Plugins.Terminal.dll";

        internal const string TERMINAL_TARGET = @"Plugins\Terminal";

        internal const string VMRC_PLUGIN = "Terminals.Plugins.Vmrc.dll";

        internal const string VMRC_TARGET = @"Plugins\Vmrc";

        internal const string WEB_PLUGIN = "Terminals.Plugins.Web.dll";

        internal const string WEB_TARGET = @"Plugins\Web";

        internal const string RDP_PLUGIN = "Terminals.Plugins.Rdp.dll";

        internal const string RDP_TARGET = @"Plugins\Rdp";

        public TestContext TestContext { get; set; }

        protected void ClassInitialize()
        {
            string[] allAvailable = this.CreateAllAvailablePlugins();
            FilePersistedTestLab.SetDefaultFileLocations();
            Settings.Instance.EnabledPlugins = allAvailable;
        }

        protected string[] CreateAllAvailablePlugins()
        {
            string deploymentDirectory = this.TestContext.DeploymentDirectory;
            return new string[]
            {
                Path.Combine(deploymentDirectory, VNC_TARGET, VNC_PLUGIN),
                Path.Combine(deploymentDirectory, ICA_TARGET, ICA_PLUGIN),
                Path.Combine(deploymentDirectory, TERMINAL_TARGET, TERMINAL_PLUGIN),
                Path.Combine(deploymentDirectory, VMRC_TARGET, VMRC_PLUGIN),
                Path.Combine(deploymentDirectory, WEB_TARGET, WEB_PLUGIN),
                Path.Combine(deploymentDirectory, RDP_TARGET, RDP_PLUGIN),
            };
        }
    }
}