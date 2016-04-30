using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using CGL.LC_Models;

using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace CGL
{
    /// <summary>
    /// LiveCodingAPI Base (BETA)
    /// </summary>
    public class CGL_LiveCodingAPI_BASE
    {
        public CGL_LiveCodingAPI_BASE()
        {
            this.BaseApiUri = new Uri("https://www.livecoding.tv/api/");
            this.ClientId = "YOUR_CLIENT_ID";
            this.ClientSecret = "YOUR_SECRET";
            this.CallbackUri = new Uri("http://www.YOURCALLBACK.com");
            this.State = System.Guid.NewGuid();
        }

        /// <summary>
        /// Get the scope string to path to the OAuth function to request permissions.
        /// </summary>
        /// <param name="scope">The scope(s) you want access to.</param>
        /// <returns>A string of scope.</returns>
        public string GetScopeString(Scope scope = Scope.Read)
        {
            string ret = "";

            int count = 0;
            foreach (Scope s in Enum.GetValues(typeof(Scope)))
            {
                // skip full_control
                if (s == Scope.FullControl || s == Scope.None)
                    continue;

                // check the bit, if the bit is set add it to the return value
                if ((scope & s) == s)
                {
                    if (count == 0)
                    {                        
                        ret += s.ToString().Replace('_', ':').ToLower();
                    }
                    else
                    {
                        ret += "+" + s.ToString().Replace('_', ':').ToLower();
                    }
                }
                count++;
            }

            return ret;
        }

        #region [Root --------------------------------------------------------]

        /// <summary>
        /// Get basic information about the API and authentication status.
        /// </summary>
        /// <returns>A Root_Response with URLs of other end points on success, else null.</returns>
        public async Task<Root_Response> GetRoot()
        {
            return await HttpAction<Root_Response>(BaseApiUri, AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        #endregion

        #region [Coding Categories -------------------------------------------]

        /// <summary>
        /// Get a list all the coding categories available.
        /// </summary>
        /// <param name="limit">The max items to return.  Default 100.</param>
        /// <param name="offset">The offset page. Default 0.</param>
        /// <returns>A CodingCategoryList_Response of all coding categories on LiveCoding on success, else null.</returns>
        public async Task<CodingCategoryList_Response> GetCodingCategories(int limit = 100, int offset = 0)
        {
            return await HttpAction<CodingCategoryList_Response>(new Uri(BaseApiUri, string.Format("codingcategories/?limit={0}&offset={1}", limit, offset)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get the Coding Category by name.
        /// </summary>
        /// <param name="name">The name to match.</param>
        /// <returns>A CodingCategory_Response matching the name on success, else null.</returns>
        public async Task<CodingCategory_Response> GetCodingCategoryByName(string name = null)
        {
            // null and length checks
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            return await HttpAction<CodingCategory_Response>(new Uri(BaseApiUri, string.Format("codingcategories/{0}/", name)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }


        #endregion

        #region [Languages ---------------------------------------------------]

        /// <summary>
        /// Get a list all the natural (human) languages known to the platform.
        /// </summary>
        /// <param name="limit">The max items to return.  Default 100.</param>
        /// <param name="offset">The offset page. Default 0.</param>
        /// <returns>Returns a List of Languages on success, else null.</returns>
        public async Task<LanguageList_Response> GetLanguages(int limit = 100, int offset = 0)
        {
            return await HttpAction<LanguageList_Response>(new Uri(BaseApiUri, string.Format("languages/?limit={0}&offset={1}", limit, offset)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get a Language by the name.
        /// </summary>
        /// <param name="name">The name to query.</param>
        /// <returns>A Language on match, else null.</returns>
        public async Task<Language_Response> GetLanguageByName(string name = null)
        {
            // null and length checks
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            return await HttpAction<Language_Response>(new Uri(BaseApiUri, string.Format("languages/{0}/", name)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        #endregion

        #region [Live Stream -------------------------------------------------]

        /// <summary>
        /// Get a list of all channels that had streaming activity.
        /// </summary>
        /// <param name="limit">The max items to return.  Default 100.</param>
        /// <param name="offset">The offset page. Default 0.</param>
        /// <returns>A LiveStreamList_Response of LiveStream on success, else null.</returns>
        public async Task<LiveStreamList_Response> GetLiveStreams(int limit = 100, int offset = 0)
        {
            return await HttpAction<LiveStreamList_Response>(new Uri(BaseApiUri, string.Format("livestreams/?limit={0}&offset={1}", limit, offset)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get all LiveStreams that are currently on air
        /// </summary>
        /// <param name="limit">The max items to return.  Default 100.</param>
        /// <param name="offset">The offset page. Default 0.</param>
        /// <returns>A LiveStreamList_Response of LiveStream on success, else null.</returns>
        public async Task<LiveStreamList_Response> GetOnAirStreams(int limit = 100, int offset = 0)
        {
            return await HttpAction<LiveStreamList_Response>(new Uri(BaseApiUri, string.Format("livestreams/onair/?limit={0}&offset={1}", limit, offset)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get the LiveStream by Name.
        /// </summary>
        /// <param name="name">The name to match.</param>
        /// <returns>A LiveStream_Response of the matching Stream on success, else null.</returns>
        public async Task<LiveStream_Response> GetStreamByName(string name = null)
        {
            // null and length check
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            return await HttpAction<LiveStream_Response>(new Uri(BaseApiUri, string.Format("livestreams/{0}/", name)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        #endregion

        #region [Scheduled Broadcast -----------------------------------------]

        /// <summary>
        /// Get a list of all the scheduled broadcasts.
        /// </summary>
        /// <param name="limit">The max items to return.  Default 100.</param>
        /// <param name="offset">The offset page. Default 0.</param>
        /// <returns>A ScheduledBroadcastList_Repsonse of ScheduledBroadcast on success, else null.</returns>
        public async Task<ScheduledBroadcastList_Response> GetScheduledBroadcast(int limit = 100, int offset = 0)
        {
            return await HttpAction<ScheduledBroadcastList_Response>(new Uri(BaseApiUri, string.Format("scheduledbroadcast/?limit={0}&offset={1}", limit, offset)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get the ScheduledBroadcast by Id.
        /// </summary>
        /// <param name="id">The Id to match.</param>
        /// <returns>A ScheduledBroadcast_Response of the matching ScheduledBroadcast on success, else null.</returns>
        public async Task<ScheduledBroadcast_Response> GetScheduledBroadcastById(int id = -1)
        {
            // id check
            if (id < 0)
            {
                return null;
            }

            return await HttpAction<ScheduledBroadcast_Response>(new Uri(BaseApiUri, string.Format("scheduledbroadcast/{0}/", id.ToString())), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        #endregion

        #region [User --------------------------------------------------------]

        /// <summary>
        /// Get related information about the current user.
        /// </summary>
        /// <returns>A User_Response of the current User on success, else null.</returns>
        public async Task<User_Response> GetCurrentUser()
        {
            return await HttpAction<User_Response>(new Uri(BaseApiUri, string.Format("{0}", "user/")), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get a list of User(s) that are following the current user.
        /// </summary>
        /// <returns>A UserList_Response of all Users that are following the current User on success, else null.</returns>
        public async Task<UserList_Response> GetCurrentUserFollowers()
        {
            return await HttpAction<UserList_Response>(new Uri(BaseApiUri, string.Format("{0}", "user/followers/")), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get a list of Users that the current user is following.
        /// </summary>
        /// <returns>A UserList_Response of all users that the current User is following on success, else null.</returns>
        public async Task<UserList_Response> GetCurrentUserFollowing()
        {
            return await HttpAction<UserList_Response>(new Uri(BaseApiUri, string.Format("{0}", "user/follows/")), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get the Viewing Key of the current user.
        /// </summary>
        /// <returns>A ViewKey_Response of the current URL and Key on success, else null.</returns>
        public async Task<ViewKey_Response> GetUserViewingKey()
        {
            // NOTE(duan): documentation @ https://www.livecoding.tv/developer/documentation/#!/user/Account_User_viewing_key is incorrect.
            return await HttpAction<ViewKey_Response>(new Uri(BaseApiUri, string.Format("{0}", "user/viewing_key/")), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get current user's XMPP account information.
        /// </summary>
        /// <returns>A XMPPAccount_Response of the current user's Chat Account on success, else null.</returns>
        public async Task<XMPPAccount_Response> GetUserChatAccount()
        {
            return await HttpAction<XMPPAccount_Response>(new Uri(BaseApiUri, string.Format("{0}", "user/chat/account/")), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get user's channel(s)
        /// </summary>
        /// <returns>An AuthenticatedLiveStreamList_Response of the current user's channels on success, else null.</returns>
        public async Task<AuthenticatedLiveStreamList_Response> GetUserChannel()
        {
            return await HttpAction<AuthenticatedLiveStreamList_Response>(new Uri(BaseApiUri, string.Format("{0}", "user/livestreams/")), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get user's channel(s) that are currently on-air.
        /// </summary>
        /// <returns>An AuthenticatedLiveStreamList_Response of the current user's channels on success, else null.</returns>
        public async Task<AuthenticatedLiveStreamList_Response> GetUserChannelOnAir()
        {
            return await HttpAction<AuthenticatedLiveStreamList_Response>(new Uri(BaseApiUri, string.Format("{0}", "user/livestreams/onair/")), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get a list of the current User's videos.
        /// </summary>
        /// <param name="limit">The max items to return.  Default 100.</param>
        /// <param name="offset">The offset page. Default 0.</param>
        /// <returns>A VideoList_Response of the current User's videos on success, else null.</returns>
        public async Task<VideoList_Response> GetUserVideos(int limit = 100, int offset = 0)
        {
            return await HttpAction<VideoList_Response>(new Uri(BaseApiUri, string.Format("user/videos/?limit={0}&offset={1}", limit, offset)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get a list of the current User's lastest videos.
        /// </summary>
        /// <returns>A VideoList_Response of the current User's videos on success, else null.</returns>
        public async Task<LatestVideoList_Response> GetUserLatestVideos()
        {
            return await HttpAction<LatestVideoList_Response>(new Uri(BaseApiUri, string.Format("{0}", "user/videos/latest/")), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        #endregion

        #region [Users -------------------------------------------------------]

        /// <summary>
        /// Provides public profile information about the specific user.
        /// </summary>
        /// <param name="username">The username to match.</param>
        /// <returns>A User_Response of the matching user's information on success, else null.</returns>
        public async Task<User_Response> GetUserInfo(string username = null)
        {
            // null and length checks
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            return await HttpAction<User_Response>(new Uri(BaseApiUri, string.Format("users/{0}/", username)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        #endregion

        #region [Videos ------------------------------------------------------]

        /// <summary>
        /// Get a list of all Vidoes on LiveCoding.
        /// </summary>
        /// <param name="limit">The max items to return.  Default 100.</param>
        /// <param name="offset">The offset page. Default 0.</param>
        /// <returns>A VideoList_Response of all Vidoes on LiveCoding on success, else null.</returns>
        public async Task<VideoList_Response> GetVideoList(int limit = 100, int offset = 0)
        {
            return await HttpAction<VideoList_Response>(new Uri(BaseApiUri, string.Format("videos/?limit={0}&offset={1}", limit, offset)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        /// <summary>
        /// Get a video by a specific Slug.
        /// </summary>
        /// <param name="slug">The slug of the video to match.</param>
        /// <returns>A Video_Response of the matching video on success, else null.</returns>
        public async Task<Video_Response> GetVideoBySlug(string slug = null)
        {
            // null and length checks
            if(string.IsNullOrWhiteSpace(slug))
            {
                return null;
            }

            return await HttpAction<Video_Response>(new Uri(BaseApiUri, string.Format("videos/{0}/", slug)), AUTHENTICATION.AUTHENTICATED, REST_ACTION.GET);
        }

        #endregion

        #region [REST Functions ----------------------------------------------]
        /// NOTE(duan): Custom REST functions for navigating the TwitchAPI
        /// 
        /// 

        public enum REST_ACTION { DELETE, GET, POST, PUT, SEND }

        public enum AUTHENTICATION { PUBLIC, AUTHENTICATED }

        /// <summary>
        /// REST operation on a specific Uri and decodes the return string into a T Object.
        /// </summary>
        /// <typeparam name="T">Type of object to decode.</typeparam>
        /// <param name="uri">Uri path.</param>
        /// <param name="authentication">AUTHENTICATION mode.</param>
        /// <param name="action">REST_ACTION operation to perform.</param>
        /// <param name="data">Object to past to the REST operation.</param>
        /// <returns>T Object from the decoded return string.</returns>
        private async Task<T> HttpAction<T>(Uri uri, AUTHENTICATION authentication = AUTHENTICATION.PUBLIC, REST_ACTION action = REST_ACTION.GET, object data = null)
        {
            T ret = default(T);

            using (HttpClient hc = new HttpClient())
            {
                switch (authentication)
                {
                    // add in OAUTH token
                    case AUTHENTICATION.AUTHENTICATED:
                        // null check
                        if (this.OAuthToken == null)
                        {
                            return ret;
                        }

                        hc.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", this.OAuthToken));
                        break;
                }

                HttpResponseMessage m = null;
                switch (action)
                {
                    case REST_ACTION.GET:
                        m = await hc.GetAsync(uri);

                        switch (m.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                // read content
                                string content = await m.Content.ReadAsStringAsync();
                                ret = GetDataFromContent<T>(ref content);
                                break;

                            case (HttpStatusCode)422:
                                // 422 Unprocessable Entity
                                // TODO(duan): log this
                                break;

                            default:
                                break;
                        }
                        break;

                    case REST_ACTION.PUT:
                        StringContent body = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                        body.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        m = await hc.PutAsync(uri, body);

                        switch (m.StatusCode)
                        {
                            case HttpStatusCode.OK:
                                string content = await m.Content.ReadAsStringAsync();
                                ret = GetDataFromContent<T>(ref content);
                                break;
                            case (HttpStatusCode)422:
                                // 422 Unprocessable Entity
                                // TODO(duan): log this
                                break;
                        }
                        break;

                    case REST_ACTION.POST:
                        m = await hc.PostAsync(uri, (FormUrlEncodedContent)data);

                        switch (m.StatusCode)
                        {
                            case HttpStatusCode.OK:
                            case HttpStatusCode.NoContent:
                                // read content
                                string content = await m.Content.ReadAsStringAsync();
                                ret = GetDataFromContent<T>(ref content);
                                break;

                            case (HttpStatusCode)422:
                            // 422 Unprocessable Entity
                            // TODO(duan): log this
                            // NOTE(duan): should pass through to the default case
                            default:
                                if (ret is bool)
                                {
                                    ret = (T)Convert.ChangeType(false, typeof(bool));
                                }
                                else
                                {
                                }
                                break;
                        }
                        break;

                    case REST_ACTION.DELETE:
                        m = await hc.DeleteAsync(uri);
                        switch (m.StatusCode)
                        {
                            case HttpStatusCode.NoContent:
                            case HttpStatusCode.OK:
                                // read content
                                string content = await m.Content.ReadAsStringAsync();
                                ret = GetDataFromContent<T>(ref content);
                                break;

                            case (HttpStatusCode)422:
                            // 422 Unprocessable Entity
                            // TODO(duan): log this
                            // NOTE(duan): should pass through to the default case
                            default:
                                if (ret is bool)
                                {
                                    ret = (T)Convert.ChangeType(false, typeof(bool));
                                }
                                break;
                        }
                        break;
                }
            }

            return ret;
        }

        /// <summary>
        /// Decode the object from the JSON content.
        /// </summary>
        /// <typeparam name="T">The Type T to convert the JSON to.</typeparam>
        /// <param name="content">The JSON content.</param>
        /// <returns>A Object of Type T from the JSON content on success, else default(T).</returns>
        private T GetDataFromContent<T>(ref string content)
        {
            T ret = default(T);
            try
            {
                if (ret is bool)
                {
                    ret = (T)Convert.ChangeType(true, typeof(bool));
                }
                else
                {
                    // decode content
                    ret = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception ex)
            {
                // TODO(duan): log error
                string error_message = ex.Message;
            }
            return ret;
        }

        #endregion

        private Uri _base_api_uri;
        [Category("LiveCoding API")]
        [DefaultValue(typeof(Uri), "https://www.livecoding.tv/api/")]
        [XmlIgnore]
        public Uri BaseApiUri
        {
            get { return _base_api_uri; }
            set
            {
                if (_base_api_uri != value)
                {
                    _base_api_uri = value;
                }
            }
        }

        [XmlAttribute("BaseApiUri")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        public string BaseApiUriString
        {
            get { return BaseApiUri == null ? null : BaseApiUri.ToString(); }
            set { BaseApiUri = value == null ? null : new Uri(value); }
        }

        private string _client_id;
        [Category("LiveCoding API")]
        [DataMember()]
        public string ClientId
        {
            get { return _client_id; }
            set
            {
                if (_client_id != value)
                {
                    _client_id = value;
                }
            }
        }

        private string _client_secret;
        [Category("LiveCoding API")]
        [DataMember()]
        public string ClientSecret
        {
            get
            {
                return _client_secret;
            }
            set
            {
                if (_client_secret != value)
                {
                    _client_secret = value;
                }
            }
        }

        private Uri _callback_uri;
        [Category("LiveCoding API")]
        [DefaultValue(typeof(Uri), "http://localhost")]
        [DataMember()]
        [XmlIgnore]
        public Uri CallbackUri
        {
            get { return _callback_uri; }
            set
            {
                if (_callback_uri != value)
                {
                    _callback_uri = value;                    
                }
            }
        }

        [XmlAttribute("CallbackUri")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string CallbackUriString
        {
            get { return CallbackUri == null ? null : CallbackUri.ToString(); }
            set { CallbackUri = value == null ? null : new Uri(value); }
        }

        private Guid _guid;
        [Category("LiveCoding API")]
        [DataMember()]
        public Guid State
        {
            get { return _guid; }
            set
            {
                if(_guid != value)
                {
                    _guid = value;
                }
            }
        }

        private string _oauth_token;
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DataMember()]
        public string OAuthToken
        {
            get { return _oauth_token; }
            set
            {
                if (_oauth_token != value)
                {
                    _oauth_token = value;
                }
            }
        }
    }

    /// <summary>
    ///  A custom attribute to allow a property to have a Category associated with it.
    /// </summary>
    public class Category : Attribute
    {
        public Category(string category)
        {
            _category_name = category;
        }

        private string _category_name;
        public string CategoryName
        {
            get { return _category_name; }
            set
            {
                if (_category_name != value)
                {
                    _category_name = value;
                }
            }
        }
    }
}
