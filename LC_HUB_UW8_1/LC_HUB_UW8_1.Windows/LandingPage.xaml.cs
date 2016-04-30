using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Xml.Serialization;

namespace LC_HUB_UW8_1
{
    /// <summary>
    /// The Statup/Landing Page for the LiveCoding Application.
    /// </summary>
    public sealed partial class LandingPage : Page
    {
        CGL.CGL_LiveCodingAPIv1 LiveCodingAPI = null;

        public LandingPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// OnNavigatedTo event.
        /// We check if the user already has login credentials save or not.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // check for login credentials           
            try
            {                
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey("API"))
                {
                    // try to deserialize the login credentials
                    LiveCodingAPI = Deserialize<CGL.CGL_LiveCodingAPIv1>(Windows.Storage.ApplicationData.Current.LocalSettings.Values["API"].ToString());
                }
                else
                {
                    // no credentials saved, they will have to log in
                }
            }
            catch (Exception ex)
            {
                string error_message = ex.Message;
            }

            // login credentials good?
            if (LiveCodingAPI != null)
            {
            }
            else
            {
                // create default API 
                LiveCodingAPI = new CGL.CGL_LiveCodingAPIv1();
            }
        }

        /// <summary>
        /// Deserialize a XML-string to a specific object.
        /// </summary>
        /// <typeparam name="T">The Type of object to deserialize.</typeparam>
        /// <returns>An T object from the XML-string on success, else default(T).</returns>
        public static T Deserialize<T>(string xml)
        {
            try
            {
                using (var sw = new StringReader(xml))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(sw);
                }
            }
            catch (Exception ex)
            {
                string error_message = ex.Message;
            }

            return default(T);
        }

        /// <summary>
        /// Move the user to the OAUTH page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LiveCodingLogin), LiveCodingAPI);
        }

        /// <summary>
        /// OnLoaded, determine if we have some credential saved.  If so, we need to check to see if it's still valid.
        /// If it is still valid we move them to the MainPage, else we move them to the OAUTH login page to refresh their token.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LC_LandingPage_Loaded(object sender, RoutedEventArgs e)
        {
            // login credentials good?
            if (LiveCodingAPI.OAuthToken != null)
            {
                // we need to test the token
                CGL.LC_Models.Root_Response GET_ROOT = await LiveCodingAPI.GetRoot();
                if(GET_ROOT == null)
                {
                    // token is bad, do nothing they will have to login again
                }
                else
                {
                    // token is good
                    this.Frame.Navigate(typeof(MainPage), LiveCodingAPI);
                }
            }
        }
    }
}
