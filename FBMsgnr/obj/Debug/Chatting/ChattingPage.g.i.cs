﻿#pragma checksum "D:\FacebookMsgner\Internal\FBMsgnr\FBMsgnr\Chatting\ChattingPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "544BAD6751AF7DFB772D44A70F2AFCBB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;


namespace FBMsgnr.Chatting {
    
    
    public partial class ChattingPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock txt_FacebookFriendName;
        
        internal System.Windows.Controls.TextBlock txt_ConversationState;
        
        internal Telerik.Windows.Controls.RadConversationView conversationView;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/FBMsgnr;component/Chatting/ChattingPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.txt_FacebookFriendName = ((System.Windows.Controls.TextBlock)(this.FindName("txt_FacebookFriendName")));
            this.txt_ConversationState = ((System.Windows.Controls.TextBlock)(this.FindName("txt_ConversationState")));
            this.conversationView = ((Telerik.Windows.Controls.RadConversationView)(this.FindName("conversationView")));
        }
    }
}

