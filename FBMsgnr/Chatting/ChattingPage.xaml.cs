using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using FBMsgnr.Chat.ViewModels;
using XFacebook.Chat.WindowsPhone.XFACEBOOK;


namespace FBMsgnr.Chatting
{
    public partial class ChattingPage : PhoneApplicationPage
    {
        public ChattingPage()
        {
            InitializeComponent();
            Loaded+=ChattingPage_Loaded;
        }

        void ChattingPage_Loaded(object sender, RoutedEventArgs e)
        {
            Global.isFromChatPage = true;
            txt_FacebookFriendName.Text = Global.GSelectedFriendName;

            this.SetConversationParticipants();

            App.iXFacebook.OnLoadedMessageReceived += iXFacebook_OnLoadedMessageReceived;
            App.iXFacebook.OnLoadedUserConversationStateChanged += iXFacebook_OnLoadedUserConversationStateChanged;

        }

        void iXFacebook_OnLoadedUserConversationStateChanged(object sender, ConversationStateItemsArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                txt_ConversationState.Text = e.iConversationStateItems.State;
            });
        }


        void iXFacebook_OnLoadedMessageReceived(object sender, NewMessageItemsArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {

                if (e.iNewMessageItems.IsReceived)
                {
                    MessagesViewModel viewModel = this.DataContext as MessagesViewModel;
                    CustomMessage customMessage = new CustomMessage(e.iNewMessageItems.Message, DateTime.Now.AddMinutes(1), ConversationViewMessageType.Incoming, viewModel.ConversationBuddy.PersonId);
                    viewModel.Messages.Add(customMessage);
                }
            });
        }




        private void SetConversationParticipants()
        {
            MessagesViewModel viewModel = this.DataContext as MessagesViewModel;
            viewModel.ConversationBuddy = viewModel.People[0];
            viewModel.You = viewModel.People[1];
        }


        private void OnSendingMessage(object sender, ConversationViewMessageEventArgs e)
        {
            if (string.IsNullOrEmpty((e.Message as ConversationViewMessage).Text))
            {
                return;
            }
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                ConversationViewMessage originalMessage = e.Message as ConversationViewMessage;
                MessagesViewModel viewModel = this.DataContext as MessagesViewModel;
                CustomMessage customMessage = new CustomMessage(originalMessage.Text, originalMessage.TimeStamp, originalMessage.Type, viewModel.You.PersonId);
                viewModel.Messages.Add(customMessage);

                App.iXFacebook.SendMessage(originalMessage.Text, Global.GSelectedFriendId);
            });



        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {

        }

        
    }
}