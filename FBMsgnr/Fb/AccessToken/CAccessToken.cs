using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FBMsgnr.Fb.AccessToken
{
    class CAccessToken : IAccessToken
    {
        private string AccessToken { get; set; }

        CAccessTokenArgs iCAccessTokenArgs = new CAccessTokenArgs();

        public event AccessTokenLoaded OnAccessTokenLoaded;

        public void GetAccessToken(string aUrl)
        {

            WebRequest request = WebRequest.Create(aUrl);
            request.BeginGetResponse(new AsyncCallback(this.ResponseCallback_AccessToken), request);
        }
        private void ResponseCallback_AccessToken(IAsyncResult asynchronousResult)
        {


            try
            {
                var request = (HttpWebRequest)asynchronousResult.AsyncState;

                using (var resp = (HttpWebResponse)request.EndGetResponse(asynchronousResult))
                {
                    using (var streamResponse = resp.GetResponseStream())
                    {
                        using (var streamRead = new StreamReader(streamResponse))
                        {
                            string responseString = streamRead.ReadToEnd();

                            string[] splitAccessToken = responseString.Split(new Char[] { '=', '&' });

                            string accessTokenString = splitAccessToken.GetValue(0).ToString();
                            string accessTokenValue = splitAccessToken.GetValue(1).ToString();
                            string accessTokenValueTemp = splitAccessToken.GetValue(2).ToString();
                            if (accessTokenString == "access_token")
                            {
                                AccessToken = accessTokenValue;

                            }

                        }
                    }
                }
            }
            catch (WebException ex)
            {

            }

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                ParseAccessToken(AccessToken);
            });


        }

        private void ParseAccessToken(string aAccessToken)
        {
            if (!string.IsNullOrEmpty(aAccessToken))
            {
                iCAccessTokenArgs.AccessToken = aAccessToken;

                if (OnAccessTokenLoaded != null)
                    OnAccessTokenLoaded(this, iCAccessTokenArgs);

            }
            else
                return;
        }



    }
}
