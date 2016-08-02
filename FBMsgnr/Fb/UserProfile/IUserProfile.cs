using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBMsgnr.Fb.UserProfile
{
    public delegate void UserProfileLoaded(object sender, CUserProfileArgs e);
    public interface IUserProfile
    {
        event UserProfileLoaded OnUserProfileLoaded;
        void GetUserProfile(string aAccessToken);

    }
}
