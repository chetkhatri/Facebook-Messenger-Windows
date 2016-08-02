using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBMsgnr.Fb.UserProfile
{
    public class CUserProfileArgs : EventArgs
    {
        public RootObject RootObjectArgs = new RootObject();
    }


    public class Data
    {
        public string url { get; set; }
        public bool is_silhouette { get; set; }
    }

    public class Picture
    {
        public Data data { get; set; }
    }

    public class RootObject
    {
        public string name { get; set; }
        public string id { get; set; }
        public Picture picture { get; set; }
    }
}
