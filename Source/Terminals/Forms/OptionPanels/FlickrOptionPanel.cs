using System;
using System.Windows.Forms;
using FlickrNet;
using Terminals.Configuration;
using Terminals.Services;

namespace Terminals.Forms
{
    internal partial class FlickrOptionPanel : UserControl, IOptionPanel
    {
        private String tempFrob;

        public FlickrOptionPanel()
        {
            InitializeComponent();
        }

        public void LoadSettings()
        {
            // nothing to do
        }

        public void SaveSettings()
        {
            // nothing to do
        }

        private void AuthorizeFlickrButton_Click(object sender, EventArgs e)
        {
            Flickr flickr = CaptureManager.Capture.CreateFlickerInstance();
            this.tempFrob = flickr.AuthGetFrob();
            String flickrUrl = flickr.AuthCalcUrl(this.tempFrob, AuthLevel.Write);
            ExternalLinks.OpenPath(flickrUrl);
            CompleteAuthButton.Enabled = true;
        }

        private void CompleteAuthButton_Click(object sender, EventArgs e)
        {
            Flickr flickr = CaptureManager.Capture.CreateFlickerInstance();
            try
            {
                // use the temporary Frob to get the authentication
                Auth auth = flickr.AuthGetToken(this.tempFrob);

                // Store this Token for later usage,
                // or set your Flickr instance to use it.
                MessageBox.Show("User authenticated successfully");
                Logging.Info(String.Format("User authenticated successfully. Authentication token is {0}. User id is {1}, username is {2}", auth.Token, auth.User.UserId, auth.User.Username));
                flickr.AuthToken = auth.Token;
                Settings.Instance.FlickrToken = auth.Token;
            }
            catch (FlickrException ex)
            {
                // If user did not authenticat your application
                // then a FlickrException will be thrown.
                Logging.Info("User not authenticated successfully", ex);
                MessageBox.Show("User did not authenticate you" + ex.Message);
            }
        }
    }
}
