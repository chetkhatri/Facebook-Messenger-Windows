﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using XFacebook.Chat.WindowsPhone.XFACEBOOK;

namespace FBMsgnr
{
    public class Global
    { 
        //facebook and XMPP
        public const string FacebokKey = "xxxxxxxxxxxxx"; // Put your Facebook API Key
        public const string FacebokSecret = "xxxxxxxxxxxxx"; // Put your Facebook API Secret Key

        public static string GAccessToken = String.Empty;
        public static string GUserId = String.Empty;
        public static string GUserName = String.Empty;
        public static string GUserPicture = String.Empty;

        public static string GSelectedFriendId = string.Empty;
        public static string GSelectedFriendName = string.Empty;

        public static bool isFromChatPage = false;


       
        
    }

   
}
