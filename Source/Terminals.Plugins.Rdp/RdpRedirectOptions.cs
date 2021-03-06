using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Terminals.Data
{
    [Serializable]
    public class RdpRedirectOptions
    {
        public Boolean Ports { get; set; }
        public Boolean Printers { get; set; }
        public Boolean SmartCards { get; set; }
        public Boolean Devices { get; set; }
        
        private RemoteSounds sounds = RemoteSounds.DontPlay;
        public RemoteSounds Sounds
        {
            get
            {
                return sounds;
            }
            set
            {
                sounds = value;
            }
        }

        private bool clipboard = true;
        public Boolean Clipboard
        {
            get
            {
                return clipboard;
            }
            set
            {
                clipboard = value;
            }
        }

        public String drives { get; set; }
        [XmlIgnore]
        public List<String> Drives
        {
            get
            {
                List<String> outputList = new List<String>();
                if (!String.IsNullOrEmpty(drives))
                {
                    String[] driveArray = drives.Split(";".ToCharArray());
                    foreach (String drive in driveArray)
                    {
                        outputList.Add(drive);
                    }
                }

                return outputList;
            }
            set
            {
                String newdrives = String.Empty;
                for (Int32 i = 0; i < value.Count; i++)
                {
                    newdrives += value[i];
                    if (i < value.Count - 1)
                        newdrives += ";";
                }

                drives = newdrives;
            }
        }

        internal RdpRedirectOptions Copy()
        {
            return new RdpRedirectOptions
                {
                    Ports = this.Ports,
                    Printers = this.Printers,
                    SmartCards = this.SmartCards,
                    Devices = this.Devices,
                    Sounds = this.Sounds,
                    Clipboard = this.Clipboard,
                    drives = this.drives
                };
        }
    }
}
