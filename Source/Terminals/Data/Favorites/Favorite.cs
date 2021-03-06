using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
using Terminals.Common.Connections;
using Terminals.Converters;

namespace Terminals.Data
{
    [Serializable]
    public class Favorite : IFavorite
    {
        private Guid id = Guid.NewGuid();
        [XmlAttribute("id")]
        public Guid Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }


        /// <summary>
        /// Gets or sets its associated groups container. Used to resolve associated groups membership.
        /// </summary>
        private IFavoriteGroups groups;

        public string Name { get; set; }

        /// <summary>
        /// Only to identify groups containing this favorite. Manipulating this property 
        /// has no effect in persistence layer
        /// </summary>
        [XmlIgnore]
        List<IGroup> IFavorite.Groups
        {
            get { return this.GetGroups(); }
        }

        private string protocol = KnownConnectionConstants.RDP;
        public String Protocol
        {
            get { return protocol; }
            set
            {
                protocol = value;
            }
        }

        private int port = KnownConnectionConstants.RDPPort;
        public Int32 Port
        {
            get { return port; }
            set
            {
                port = value;
            }
        }

        public string ServerName { get; set; }

        private SecurityOptions security = new SecurityOptions();
        /// <summary>
        /// Gets or sets the user credits. Only for serialization purposes.
        /// General access is done by interface property
        /// </summary>
        public SecurityOptions Security
        {
            get { return this.security; }
            set { this.security = value; }
        }

        ISecurityOptions IFavorite.Security
        {
            get { return this.security; }
        }

        private string toolBarIconFile;

        /// <summary>
        /// Gets or sets the absolute path to the tool bar icon file, if custom icon was assigned.
        /// To directly access the icon image use <see cref="ToolBarIconImage"/>
        /// </summary>
        public string ToolBarIconFile
        {
            get
            {
                return this.toolBarIconFile;
            }
            set
            {
                this.toolBarIconFile = value;
                // don't dispose the previous image here, because it can be loaded from default shared FavoriteIcons
                this.ToolBarIconImage = null;
            }
        }

        /// <summary>
        /// Gets or sets the loaded image. Used as cache to prevent redundant loading from disk.
        /// </summary>
        [XmlIgnore]
        public Image ToolBarIconImage { get; set; }

        public Boolean NewWindow { get; set; }

        public String DesktopShare { get; set; }

        private IBeforeConnectExecuteOptions executeBeforeConnect = new BeforeConnectExecuteOptions();
        /// <summary>
        /// Only for serialization
        /// </summary>
        public BeforeConnectExecuteOptions ExecuteBeforeConnect
        {
            get { return this.executeBeforeConnect as BeforeConnectExecuteOptions; }
            set { this.executeBeforeConnect = value; }
        }

        IBeforeConnectExecuteOptions IFavorite.ExecuteBeforeConnect
        {
            get { return this.executeBeforeConnect; }
        }

        private IDisplayOptions display = new DisplayOptions();
        /// <summary>
        /// Only for serialization.
        /// </summary>
        public DisplayOptions Display
        {
            get { return this.display as DisplayOptions; }
            set { this.display = value; }
        }

        IDisplayOptions IFavorite.Display
        {
            get { return this.display; }
        }

        private ProtocolOptions protocolProperties = new EmptyOptions();
        /// <summary>
        /// Depending on selected protocol, this should contain the protocol detailed options.
        /// Because default protocol is RDP, also this properties are RdpOptions by default.
        /// This property should be never null, use EmptyProperties to provide in not necessary case.
        /// </summary>
        [XmlElement(Namespace = FavoritesFile.SERIALIZATION_DEFAULT_NAMESPACE)]
        public ProtocolOptions ProtocolProperties
        {
            get { return protocolProperties; }
            // setter should be used for deserialization only
            set { protocolProperties = value; }
        }

        private string notes;

        string IFavorite.Notes
        {
            get { return this.notes; }
            set { this.notes = value; }
        }

        /// <summary>
        /// Gets or sets the notes property as Base64 encoded string. This is used only for xml serialization.
        /// </summary>
        public String Notes
        {
            get
            {
                return TextConverter.EncodeTo64(this.notes);
            }
            set
            {
                this.notes = TextConverter.DecodeFrom64(value);
            }
        }

        [XmlIgnore]
        public string GroupNames
        {
            get
            {
                List<IGroup> groups = this.GetGroups();
                return GroupsListToString(groups);
            }
        }

        internal static string GroupsListToString(List<IGroup> groups)
        {
            if (groups.Count == 0)
                return string.Empty;

            string[] groupNames = groups.Select(group => @group.Name).ToArray();
            return string.Join(",", groupNames);
        }

        private List<IGroup> GetGroups()
        {
            return this.groups.GetGroupsContainingFavorite(this.Id);
        }

        /// <summary>
        /// Creates new favorite filled by properties of this favorite except Id and Groups.
        /// </summary>
        IFavorite IFavorite.Copy()
        {
            var copy = new Favorite();
            copy.UpdateFrom(this);
            return copy;
        }

        void IFavorite.UpdateFrom(IFavorite source)
        {
            var sourceFavorite = source as Favorite;
            if (sourceFavorite == null)
                return;
            this.UpdateFrom(sourceFavorite);
        }

        public void ChangeProtocol(string protocol, ProtocolOptions options)
        {
            this.protocol = protocol;
            this.protocolProperties = options;
        }

        private void UpdateFrom(Favorite source)
        {
            // we do not call AssignStores here, because they will be copied together with the child properties
            this.groups = source.groups;
            this.DesktopShare = source.DesktopShare;
            this.Display = source.Display.Copy();
            this.ExecuteBeforeConnect = source.ExecuteBeforeConnect.Copy();
            this.Name = source.Name;
            this.NewWindow = source.NewWindow;
            this.Notes = source.Notes;
            this.Port = source.Port;
            this.Protocol = source.Protocol;
            this.security = (SecurityOptions)source.security.Copy();
            this.ServerName = source.ServerName;
            this.ToolBarIconFile = source.ToolBarIconFile;

            this.ProtocolProperties = source.ProtocolProperties.Copy();
        }

        bool IStoreIdEquals<IFavorite>.StoreIdEquals(IFavorite oponent)
        {
            var oponentFavorite = oponent as Favorite;
            if (oponentFavorite == null)
                return false;

            return oponentFavorite.Id == this.Id;
        }

        public int GetStoreIdHash()
        {
            return this.Id.GetHashCode();
        }

        internal void AssignStores(IFavoriteGroups groups)
        {
            this.groups = groups;
        }

        public override String ToString()
        {
            return ToString(this);
        }

        internal static string ToString(IFavorite favorite)
        {
            return String.Format(@"Favorite:{0}({1})={2}:{3}",
                favorite.Name, favorite.Protocol, favorite.ServerName, favorite.Port);
        }
    }
}
