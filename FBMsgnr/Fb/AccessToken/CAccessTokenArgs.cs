using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBMsgnr.Fb.AccessToken
{
    public class CAccessTokenArgs : EventArgs
    {
        private string _accessToken;
        public string AccessToken
        {
            get { return _accessToken; }
            set { _accessToken = value; }
        }
    }
}