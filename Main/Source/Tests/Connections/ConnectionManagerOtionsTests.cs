﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Terminals.Common.Connections;
using Terminals.Connections;
using Terminals.Connections.ICA;
using Terminals.Connections.Rdp;
using Terminals.Connections.Terminal;
using Terminals.Connections.VMRC;
using Terminals.Connections.VNC;
using Terminals.Connections.Web;
using Terminals.Data;

namespace Tests.Connections
{
    [TestClass]
    public class ConnectionManagerOtionsTests
    {
        private static readonly ConnectionManager staticLoadingConnectionManager = new ConnectionManager(() =>
            new Dictionary<string, IConnectionPlugin>()
            {
                {KnownConnectionConstants.RDP, new RdpConnectionPlugin()},
                {KnownConnectionConstants.HTTP, new HttpConnectionPlugin()},
                {KnownConnectionConstants.HTTPS, new HttpsConnectionPlugin()},
                {VncConnectionPlugin.VNC, new VncConnectionPlugin()},
                {VmrcConnectionPlugin.VMRC, new VmrcConnectionPlugin()},
                {TelnetConnectionPlugin.TELNET, new TelnetConnectionPlugin()},
                {SshConnectionPlugin.SSH, new SshConnectionPlugin()},
                {ICAConnectionPlugin.ICA_CITRIX, new ICAConnectionPlugin()}
            });

        internal static ConnectionManager StaticLoadingConnectionManager { get { return staticLoadingConnectionManager; } }

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void NullCurrentOptionsRdpProtocol_UpdateProtocolPropertiesByProtocol_ReturnsRdpOptions()
        {
            var returned = StaticLoadingConnectionManager.UpdateProtocolPropertiesByProtocol(KnownConnectionConstants.RDP, null);
            Assert.IsInstanceOfType(returned, typeof(RdpOptions), "When creating new favorite, the options arent set yet.");
        }
        
        [TestMethod]
        public void UnknownProtocol_UpdateProtocolPropertiesByProtocol_ReturnsEmptyOptions()
        {
            var returned = StaticLoadingConnectionManager.UpdateProtocolPropertiesByProtocol("UnknonwProtocol", new ConsoleOptions());
            Assert.IsInstanceOfType(returned, typeof(EmptyOptions), "There is no option, how to switch the properties.");
        }
        
        [TestMethod]
        public void NotChanged_UpdateProtocolPropertiesByProtocol_ReturnsExpectedOptions()
        {
            var testData = new[]
            {
                new Tuple<string, ProtocolOptions>(KnownConnectionConstants.RDP, new RdpOptions()),
                new Tuple<string, ProtocolOptions>(VncConnectionPlugin.VNC, new VncOptions()),
                new Tuple<string, ProtocolOptions>(VmrcConnectionPlugin.VMRC, new VMRCOptions()),
                new Tuple<string, ProtocolOptions>(TelnetConnectionPlugin.TELNET, new ConsoleOptions()),
                new Tuple<string, ProtocolOptions>(SshConnectionPlugin.SSH, new SshOptions()),
                new Tuple<string, ProtocolOptions>(KnownConnectionConstants.HTTP, new WebOptions()),
                new Tuple<string, ProtocolOptions>(KnownConnectionConstants.HTTPS, new WebOptions()),
                new Tuple<string, ProtocolOptions>(ICAConnectionPlugin.ICA_CITRIX, new ICAOptions())
            };

            var allValid = testData.All(this.AssertTheSameInstance);
            Assert.IsTrue(allValid, "All protocols have to reflect related protocol.");
        }

        private bool AssertTheSameInstance(Tuple<string, ProtocolOptions> testCase)
        {
            ProtocolOptions returned = StaticLoadingConnectionManager.UpdateProtocolPropertiesByProtocol(testCase.Item1, testCase.Item2);
            string expected = testCase.Item2.GetType().Name;
            string returnedType = returned.GetType().Name;
            ReportChangedOptions(testCase.Item1, expected, returnedType);
            return returned.GetType().FullName == testCase.Item2.GetType().FullName;  
        }

        [TestMethod]
        public void Changed_UpdateProtocolPropertiesByProtocol_ReturnsExpectedType()
        {
            var testData = new[]
            {
                new Tuple<string, Type>(KnownConnectionConstants.RDP, typeof(RdpOptions)),
                new Tuple<string, Type>(VncConnectionPlugin.VNC, typeof(VncOptions)),
                new Tuple<string, Type>(VmrcConnectionPlugin.VMRC, typeof(VMRCOptions)),
                new Tuple<string, Type>(TelnetConnectionPlugin.TELNET, typeof(ConsoleOptions)),
                new Tuple<string, Type>(SshConnectionPlugin.SSH, typeof(SshOptions)),
                new Tuple<string, Type>(KnownConnectionConstants.HTTP, typeof(WebOptions)),
                new Tuple<string, Type>(KnownConnectionConstants.HTTPS, typeof(WebOptions)),
                new Tuple<string, Type>(ICAConnectionPlugin.ICA_CITRIX, typeof(ICAOptions))
            };

            var allValid = testData.All(this.AssertOptions);
            Assert.IsTrue(allValid, "All protocols have to reflect related protocol.");
        }

        private bool AssertOptions(Tuple<string, Type> testCase)
        {
            // No protocol uses EmptyOptions, so it is used as change from something else
            var returned = StaticLoadingConnectionManager.UpdateProtocolPropertiesByProtocol(testCase.Item1, new EmptyOptions());
            ReportCreated(returned, testCase);
            return returned.GetType().FullName == testCase.Item2.FullName;
        }

        private void ReportCreated(ProtocolOptions returned, Tuple<string, Type> testCase)
        {
            string returnedType = returned.GetType().Name;
            string protocol = testCase.Item1;
            string expected = testCase.Item2.Name;
            this.ReportChangedOptions(protocol, expected, returnedType);
        }

        private void ReportChangedOptions(string protocol, string expected, string returned)
        {
            const string FORMAT = "For protocol {0}: Expected {1} Returned {2}";
            string message = string.Format(FORMAT, protocol, expected, returned);
            this.TestContext.WriteLine(message);
        }
    }
}
