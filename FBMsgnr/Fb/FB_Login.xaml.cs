using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using FBMsgnr.Fb.AccessToken;
using FBMsgnr.Fb.UserProfile;

namespace FBMsgnr.Fb
{
    public partial class FB_Login : PhoneApplicationPage
    {
        public FB_Login()
        {
            InitializeComponent();
            Loaded += FB_Login_Loaded;
        }

        private void WebBrowserFbLogin_Navigated(object sender, NavigationEventArgs e)
        {
            txt_Connecting.Visibility = System.Windows.Visibility.Collapsed;

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (e.Uri.AbsolutePath.Equals("/connect/login_success.html"))
                {
                    string Query = e.Uri.Query.ToString();
                    string[] split = Query.Split(new Char[] { '=' });
                    string code = split.GetValue(1).ToString();
                    if (code.Length > 0)
                    {
                        WebBrowserFbLogin.Visibility = System.Windows.Visibility.Collapsed;
                        txt_Loading.Visibility = System.Windows.Visibility.Visible;

                        var url = "https://graph.facebook.com/oauth/access_token?client_id=" + Global.FacebokKey + "&redirect_uri=https://www.facebook.com/connect/login_success.html&client_secret=" + Global.FacebokSecret + "&code=" + code;

                        //get access token
                        IAccessToken iAccessToken = new CAccessToken();
                        iAccessToken.OnAccessTokenLoaded += iAccessToken_OnAccessTokenLoaded;
                        iAccessToken.GetAccessToken(url);

                    }
                }
                else
                    return;


            });
        }

        private void WebBrowserFbLogin_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            WebBrowserFbLogin.Visibility = System.Windows.Visibility.Collapsed;
            txt_Connecting.Visibility = System.Windows.Visibility.Collapsed;
            txt_Loading.Visibility = System.Windows.Visibility.Visible;
            txt_Loading.Text = "Failed to connect to Facebook";
        }

        void FB_Login_Loaded(object sender, RoutedEventArgs e)
        {
            var url = "https://www.facebook.com/dialog/oauth?client_id=" + Global.FacebokKey + "&redirect_uri=https://www.facebook.com/connect/login_success.html&scope=email,user_location,friends_location,user_hometown,friends_hometown,publish_stream,read_stream,user_status,user_photos,friends_photos,friends_status,user_checkins,friends_checkins,friends_events,user_events,publish_checkins,xmpp_login&display=touch";
            WebBrowserFbLogin.Navigate(new Uri(url));
        }

        //get access token
        void iAccessToken_OnAccessTokenLoaded(object sender, CAccessTokenArgs e)
        {
            Global.GAccessToken = e.AccessToken;

            //get user profile
            IUserProfile iUserProfile = new CUserProfile();
            iUserProfile.OnUserProfileLoaded += iUserProfile_OnUserProfileLoaded;
            iUserProfile.GetUserProfile(e.AccessToken);

        }

        //get user profile
        void iUserProfile_OnUserProfileLoaded(object sender, CUserProfileArgs e)
        {
            Global.GUserId = e.RootObjectArgs.id;
            Global.GUserName = e.RootObjectArgs.name;
            Global.GUserPicture = e.RootObjectArgs.picture.data.url;

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {

                //launch the game page
                NavigationService.Navigate(new Uri("/Friends/FriendsList.xaml", UriKind.RelativeOrAbsolute));


            });
        }
    }
}