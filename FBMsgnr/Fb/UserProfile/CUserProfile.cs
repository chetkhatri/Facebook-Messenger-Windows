using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Windows;
namespace FBMsgnr.Fb.UserProfile
{
    class CUserProfile : IUserProfile
    {
        CUserProfileArgs iCUserProfileArgs = new CUserProfileArgs();

        public event UserProfileLoaded OnUserProfileLoaded;

        public void GetUserProfile(string aAccessToken)
        {
            var urlProfile = "https://graph.facebook.com/me?fields=name,picture&access_token=" + aAccessToken;

            WebRequest request = WebRequest.Create(urlProfile); //FB access token  Link
            request.BeginGetResponse(new AsyncCallback(this.ResponseCallbackProfile), request);

        }

        private void ResponseCallbackProfile(IAsyncResult asynchronousResult)
        {
            try
            {
                var request = (HttpWebRequest)asynchronousResult.AsyncState;

                using (var resp = (HttpWebResponse)request.EndGetResponse(asynchronousResult))
                {


                    using (Stream streamResponse = resp.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(streamResponse, Encoding.UTF8);
                        String responseString = reader.ReadToEnd();
                        var rootObject = JsonConvert.DeserializeObject<RootObject>(responseString);
                        if (!string.IsNullOrEmpty(rootObject.id) && !string.IsNullOrEmpty(rootObject.name) && !string.IsNullOrEmpty(rootObject.picture.data.url))
                        {
                            iCUserProfileArgs.RootObjectArgs = rootObject;

                            if (OnUserProfileLoaded != null)
                                OnUserProfileLoaded(this, iCUserProfileArgs);
                        }

                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
    }
}