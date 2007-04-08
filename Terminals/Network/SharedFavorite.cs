using System;
using System.Collections.Generic;
using System.Text;

namespace Terminals.Network {
    [Serializable()]
    public class SharedFavorite {
        public static Terminals.FavoriteConfigurationElement ConvertFromFavorite(SharedFavorite Favorite) {
            Terminals.FavoriteConfigurationElement fav = new Terminals.FavoriteConfigurationElement();
            fav.Colors = Favorite.Colors;
            fav.ConnectToConsole = Favorite.ConnectToConsole;
            fav.DesktopShare = Favorite.DesktopShare;
            fav.DesktopSize = Favorite.DesktopSize;
            fav.DomainName = Favorite.DomainName;
            fav.Name = Favorite.Name;
            fav.Port = Favorite.Port;
            fav.Protocol = Favorite.Protocol;
            fav.RedirectClipboard = Favorite.RedirectClipboard;
            fav.RedirectDevices = Favorite.RedirectDevices;
            fav.RedirectDrives = Favorite.RedirectDrives;
            fav.RedirectPorts = Favorite.RedirectPorts;
            fav.RedirectPrinters = Favorite.RedirectPrinters;
            fav.RedirectSmartCards = Favorite.RedirectSmartCards;
            fav.ServerName = Favorite.ServerName;
            fav.ShowDesktopBackground = Favorite.ShowDesktopBackground;
            fav.Sounds = Favorite.Sounds;
            fav.Tags = Favorite.Tags;
            fav.Telnet = Favorite.Telnet;
            fav.TelnetBackColor = Favorite.TelnetBackColor;
            fav.TelnetCols = Favorite.TelnetCols;
            fav.TelnetCursorColor = Favorite.TelnetCursorColor;
            fav.TelnetFont = Favorite.TelnetFont;
            fav.TelnetRows = Favorite.TelnetRows;
            fav.TelnetTextColor = Favorite.TelnetTextColor;
            fav.VMRCAdministratorMode = Favorite.VMRCAdministratorMode;
            fav.VMRCReducedColorsMode = Favorite.VMRCReducedColorsMode;

            return fav;
        }
        public static SharedFavorite ConvertFromFavorite(Terminals.FavoriteConfigurationElement Favorite) {
            SharedFavorite fav = new SharedFavorite();
            fav.Colors = Favorite.Colors;
            fav.ConnectToConsole = Favorite.ConnectToConsole;
            fav.DesktopShare = Favorite.DesktopShare;
            fav.DesktopSize = Favorite.DesktopSize;
            fav.DomainName = Favorite.DomainName;
            fav.Name = Favorite.Name;
            fav.Port = Favorite.Port;
            fav.Protocol = Favorite.Protocol;
            fav.RedirectClipboard = Favorite.RedirectClipboard;
            fav.RedirectDevices = Favorite.RedirectDevices;
            fav.RedirectDrives = Favorite.RedirectDrives;
            fav.RedirectPorts = Favorite.RedirectPorts;
            fav.RedirectPrinters = Favorite.RedirectPrinters;
            fav.RedirectSmartCards = Favorite.RedirectSmartCards;
            fav.ServerName = Favorite.ServerName;
            fav.ShowDesktopBackground = Favorite.ShowDesktopBackground;
            fav.Sounds = Favorite.Sounds;
            fav.Tags = Favorite.Tags;
            fav.Telnet = Favorite.Telnet;
            fav.TelnetBackColor = Favorite.TelnetBackColor;
            fav.TelnetCols = Favorite.TelnetCols;
            fav.TelnetCursorColor = Favorite.TelnetCursorColor;
            fav.TelnetFont = Favorite.TelnetFont;
            fav.TelnetRows = Favorite.TelnetRows;
            fav.TelnetTextColor = Favorite.TelnetTextColor;
            fav.VMRCAdministratorMode = Favorite.VMRCAdministratorMode;
            fav.VMRCReducedColorsMode = Favorite.VMRCReducedColorsMode;
            return fav;
        }
        public bool VMRCReducedColorsMode;
        public bool Telnet;
        public int TelnetRows;
        public int TelnetCols;
        public bool VMRCAdministratorMode;
        public string Protocol;
        public string TelnetFont;
        public string TelnetBackColor;
        public string TelnetTextColor;
        public string TelnetCursorColor;
        public string Name;
        public string ServerName;
        public string DomainName;
        public bool ConnectToConsole;
        public DesktopSize DesktopSize;
        public Colors Colors;
        public RemoteSounds Sounds;
        public bool RedirectDrives;
        public bool RedirectPorts;
        public bool RedirectPrinters;
        public bool RedirectSmartCards;
        public bool RedirectClipboard;
        public bool RedirectDevices;
        public int Port;
        public string DesktopShare;
        public bool ShowDesktopBackground;
        public string Tags;
    }
}