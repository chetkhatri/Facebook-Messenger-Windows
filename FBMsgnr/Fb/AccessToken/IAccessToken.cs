using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBMsgnr.Fb.AccessToken
{
    public delegate void AccessTokenLoaded(object sender, CAccessTokenArgs e);
    public interface IAccessToken
    {
        event AccessTokenLoaded OnAccessTokenLoaded;
        void GetAccessToken(string aUrl);
    }
}
