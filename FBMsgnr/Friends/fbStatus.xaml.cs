using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using Tools;

namespace FBMsgnr.Friends
{
    public partial class fbStatus : PhoneApplicationPage
    {
        public fbStatus()
        {
            InitializeComponent();
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            FacebookClient.Instance.PostMessageOnWall(txtStatus.Text, new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
        }
        void PostMessageOnWallCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;
            if (e.Error != null)
            {
                MessageBox.Show("Error Occurred: " + e.Error.Message);
                return;
            }

            System.Diagnostics.Debug.WriteLine(e.Result);

            string result = e.Result;
            byte[] data = Encoding.UTF8.GetBytes(result);
            MemoryStream memStream = new MemoryStream(data);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ResponseData));
            ResponseData responseData = (ResponseData)serializer.ReadObject(memStream);

            if (responseData.id != null && !responseData.id.Equals(""))
            {
                // Success
                MessageBox.Show("Message Posted!");
            }
            else if (responseData.error != null && responseData.error.code == 190)
            {
                if (responseData.error.error_subcode == 463)
                {
                    // Access Token Expired, need to get new token
                    FacebookClient.Instance.ExchangeAccessToken(new UploadStringCompletedEventHandler(ExchangeAccessTokenCompleted));
                }
                else
                {
                    // Another Error with Access Token, need to clear the Access Token
                    FacebookClient.Instance.AccessToken = "";
//                    SetLoggedInState(false);
                }
            }
            else
            {
                // Error
            }
        }

        void ExchangeAccessTokenCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            // Acquire access_token and expires timestamp
            IEnumerable<KeyValuePair<string, string>> pairs = UriToolKits.ParseQueryString(e.Result);
            string accessToken = KeyValuePairUtils.GetValue(pairs, "access_token");

            if (accessToken != null && !accessToken.Equals(""))
            {
                MessageBox.Show("Access Token Exchange Failed");
                return;
            }

            // Save access_token
            FacebookClient.Instance.AccessToken = accessToken;
            FacebookClient.Instance.PostMessageOnWall(txtStatus.Text, new UploadStringCompletedEventHandler(PostMessageOnWallCompleted));
        }


       
    }
}