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


using System.Collections.ObjectModel;

using CGL;
using CGL.LC_Models;

using System.Threading.Tasks;
using System.Xml.Serialization;


using LC_HUB_UW8_1.ViewModels;

namespace LC_HUB_UW8_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CGL_LiveCodingAPIv1 LiveCodingAPI;

        VM_CodingCategory _vm_coding_category = new VM_CodingCategory();
        VM_Language _vm_language = new VM_Language();
        VM_LiveStream _vm_livestream = new VM_LiveStream();
        VM_Video _vm_video = new VM_Video();

        public MainPage()
        {
            this.InitializeComponent();
            LiveCodingAPI = new CGL_LiveCodingAPIv1();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is CGL.CGL_LiveCodingAPIv1)
            {
                LiveCodingAPI = (e.Parameter as CGL.CGL_LiveCodingAPIv1);
            }

            base.OnNavigatedTo(e);
        }

        private async void UnitTest_Page_Loaded(object sender, RoutedEventArgs e)
        {
            // NOTE(duan): get root
            Root_Response GET_ROOT = await LiveCodingAPI.GetRoot();

            // NOTE(duan): get coding categories
            CodingCategoryList_Response GET_CODING_CATEGORIES =
            await LiveCodingAPI.GetCodingCategories();
            // build the ViewModel
            _vm_coding_category.DataSource = new ObservableCollection<CodingCategory>(GET_CODING_CATEGORIES.CodingCategories);
            // set the datacontext
            HS_CodingCategory.DataContext = _vm_coding_category;

            CodingCategory_Response GET_SINGLE_CODE_CATEGORY = null;
            if(GET_CODING_CATEGORIES != null)
            {
                if(GET_CODING_CATEGORIES.CodingCategories.Count > 0)
                {
                    GET_SINGLE_CODE_CATEGORY = await LiveCodingAPI.GetCodingCategoryByName(GET_CODING_CATEGORIES.CodingCategories[0].Name);
                }

            }

            // NOTE(duan): get languages
            LanguageList_Response GET_LANGUAGES = await LiveCodingAPI.GetLanguages();
            // build ViewModel
            _vm_language.DataSource = new ObservableCollection<Language>(GET_LANGUAGES.Languages);
            // set the datacontext
            HS_Language.DataContext = _vm_language;

            // NOTE(duan): get a single language
            Language_Response GET_LANGUAGE_BY_NAME = await LiveCodingAPI.GetLanguageByName("French");

            // NOTE(duan): get a list of livestreams
            // LiveStreamList_Response GET_LIVESTREAMS = await LiveCodingAPI.GetLiveStreams();
            LiveStreamList_Response GET_LIVESTREAMS = await LiveCodingAPI.GetOnAirStreams();
            // build the ViewModel
            _vm_livestream.DataSource = new ObservableCollection<LiveStream>(GET_LIVESTREAMS.LiveStreams);
            // set the datacontext
            HS_LiveStream.DataContext = _vm_livestream;

            // NOTE(duan): get a single stream
            LiveStream_Response GET_STREAM = await LiveCodingAPI.GetStreamByName("chubosaurus");

            // NOTE(duan): get scheduled broadcasts
            ScheduledBroadcastList_Response GET_SCHEDULED = await LiveCodingAPI.GetScheduledBroadcast();

            // NOTE(duan): get scheduled broadcast by id
            ScheduledBroadcast_Response GET_SCHEDULED_BROADCAST_BY_ID = null;
            if (GET_SCHEDULED != null)
            {
                if (GET_SCHEDULED.ScheduledBroadcasts.Count > 0)
                {
                    GET_SCHEDULED_BROADCAST_BY_ID = await LiveCodingAPI.GetScheduledBroadcastById(GET_SCHEDULED.ScheduledBroadcasts[0].Id);
                }
            }

            // NOTE(duan): get current user info
            User_Response GET_USER = await LiveCodingAPI.GetCurrentUser();

            // NOTE(duan): get followers
            UserList_Response GET_USER_FOLLOWER = await LiveCodingAPI.GetCurrentUserFollowers();

            // NOTE(duan): get following
            UserList_Response GET_USER_FOLLOW_LIST = await LiveCodingAPI.GetCurrentUserFollowing();

            // NOTE(duan): get viewing key
            ViewKey_Response GET_VK = await LiveCodingAPI.GetUserViewingKey();

            // NOTE(duan): get chat account
            XMPPAccount_Response GET_CHAT_ACCOUNT = await LiveCodingAPI.GetUserChatAccount();

            // NOTE(duan): get the user's channel
            AuthenticatedLiveStreamList_Response GET_USER_CHANNEL = await LiveCodingAPI.GetUserChannel();

            // NOTE(duan): get the user's channel (on-air)
            AuthenticatedLiveStreamList_Response GET_USER_CHANNEL_ON_AIR = await LiveCodingAPI.GetUserChannelOnAir();

            // NOTE(duan): get the user's videos
            VideoList_Response GET_USER_VIDEOS = await LiveCodingAPI.GetUserVideos();
            LatestVideoList_Response GET_USER_LATEST_VIDEOS = await LiveCodingAPI.GetUserLatestVideos();
            // build the ViewModel
            //_vm_video.DataSource = new ObservableCollection<Video>(GET_USER_VIDEOS.Videos);
            _vm_video.DataSource = new ObservableCollection<Video>(GET_USER_LATEST_VIDEOS);
            // set the datacontext
            HS_Videos.DataContext = _vm_video;

            // NOTE(duan): get public user information
            User_Response PUBLIC_USER = await LiveCodingAPI.GetUserInfo("chubosaurus");

            // NOTE(duan): get all vidoes
            VideoList_Response ALL_VIDEOS = await LiveCodingAPI.GetVideoList();
            Video_Response SINGLE_VIDEO = null;
            if(ALL_VIDEOS != null)
            {
                if(ALL_VIDEOS.Videos.Count > 0)
                {
                    SINGLE_VIDEO = await LiveCodingAPI.GetVideoBySlug(ALL_VIDEOS.Videos[0].Slug);
                }
            }
        }
    }
}
