using LC_HUB_UW8_1.Common;
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

using CGL;
using CGL.LC_Models;


namespace LC_HUB_UW8_1
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class LiveCodingLogin : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private CGL_LiveCodingAPI_BASE LiveCodingAPI;

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public LiveCodingLogin()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="Common.NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="Common.SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="Common.NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);

            // attemp to redirect the user to OAUTH login page with the requested permissions
            if (e.Parameter is CGL.CGL_LiveCodingAPI_BASE)
            {
                LiveCodingAPI = e.Parameter as CGL_LiveCodingAPI_BASE;

                // build the location
                Uri AUTH_LOCATION = new Uri(new Uri("https://www.livecoding.tv/"), string.Format("o/authorize/?scope={0}&state={1}&redirect_uri={2}&response_type=token&client_id={3}",
                    LiveCodingAPI.GetScopeString(Scope.FullControl), 
                    LiveCodingAPI.State, LiveCodingAPI.CallbackUri.OriginalString, LiveCodingAPI.ClientId));

                // setup the WebView with the right credentials
                LC_WebView.Navigate(AUTH_LOCATION);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        /// <summary>
        /// Login script for OAUTHv2 using a WebView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LC_WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (LiveCodingAPI != null)
            {
                Uri e = args.Uri;
                
                // check to see if we at the callback URL stage
                if (e.AbsoluteUri.StartsWith(LiveCodingAPI.CallbackUri.OriginalString))
                {
                    // check the callback ?
                    int q_index = e.AbsoluteUri.IndexOf('?', LiveCodingAPI.CallbackUri.OriginalString.Length);
                    // for errors
                    if (q_index > 0)
                    {
                        // TODO(duan): log, maybe alert box?
                        string error_message = "Unknown error";

                        try
                        {
                            WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(e.Query);
                            error_message = decoder.GetFirstValueByName("error");
                        }
                        catch (Exception ex)
                        {
                            error_message = ex.Message;
                        }                        

                        NavigationHelper.GoBack();
                    }
                    else
                    {
                        string error_message = "ERROR_SUCCESS";

                        // we got good feedback attemp to decode
                        // check the callback ?
                        int sharp_index = e.AbsoluteUri.IndexOf('#', LiveCodingAPI.CallbackUri.OriginalString.Length);

                        if (sharp_index > 0)
                        {
                            // parse the query string
                            string query = e.AbsoluteUri.Substring(sharp_index + 1);

                            try
                            {
                                WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(query);

                                // valid check
                                LiveCodingAPI.OAuthToken = decoder.GetFirstValueByName("access_token");
                                //LiveCodingAPI.State = decoder.GetFirstValueByName("state");
                                //decoder.GetFirstValueByName("access_token")
                                //decoder.GetFirstValueByName("token_type")
                                //decoder.GetFirstValueByName("state")
                                //decoder.GetFirstValueByName("expires_in")
                                //decoder.GetFirstValueByName("scope")

                                // save credential to local storage
                                Windows.Storage.ApplicationData.Current.LocalSettings.Values["API"] = Serialize(LiveCodingAPI);
                            }
                            catch (Exception ex)
                            {
                                error_message = ex.Message;

                                // error parsing the token, return to login page
                                this.Frame.GoBack();
                            }
                        }

                        // we good goto main page
                        this.Frame.Navigate(typeof(MainPage), LiveCodingAPI);
                    }
                }
            }
        }

        /// <summary>
        /// Serialize the object into a XML string.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>A XML-string representing the object on success, else null.</returns>
        public static string Serialize(object obj)
        {
            using (var sw = new StringWriter())
            {
                try
                {
                    var serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(sw, obj);
                    return sw.ToString();
                }
                catch (Exception ex)
                {
                    string error_message = ex.Message;
                }
                return null;
            }
        }
    }
}
