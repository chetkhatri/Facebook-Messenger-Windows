using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using XFacebook.Chat.WindowsPhone.XFACEBOOK;
using System.Collections.ObjectModel;
namespace FBMsgnr.Friends
{
    public partial class FriendsList : PhoneApplicationPage
    {
        public FriendsList()
        {
            InitializeComponent();
            Loaded += FriendsList_Loaded;
            
        }
        void FriendsList_Loaded(object sender, RoutedEventArgs e)
        {
            
            BitmapImage bmp_UserPicture = new BitmapImage();
            bmp_UserPicture.UriSource = new Uri(Global.GUserPicture, UriKind.RelativeOrAbsolute);
            img_UserPicture.Source = bmp_UserPicture;
            txt_UserName.Text = Global.GUserName;


            App.iXFacebook.OnLoadedOnlineFriends += iXFacebook_OnLoadedOnlineFriends;

            if (!Global.isFromChatPage)
            {

                App.iXFacebook.Connect(Global.GAccessToken, Global.FacebokKey);
                
            }

        }

        void iXFacebook_OnLoadedOnlineFriends(object sender, FriendsStatusItemsArgs e)
        {

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {

                txt_OnlineFriends.Text = "Messages Menu";
                ListBoxFriends.ItemsSource = e.ListFriendsStatusItems;
                txt_OnlineFriends1.Text = "Active";
                ListBoxFriends1.ItemsSource = e.ListFriendsStatusItems.Where(x => x.Status == "available");

            });

        }

        private void ListBoxFriends_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (sender as ListBox);
            listBox.SelectedIndex = -1;
        }
        private void ListBoxFriends1_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox listBox = (sender as ListBox);
            listBox.SelectedIndex = -1;
        }
        private void ListBoxFriends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                object selectedItem = (FriendsStatusItems)e.AddedItems[0];
                ((ListBox)sender).SelectedIndex = -1;
                FrameworkElement root = Application.Current.RootVisual as FrameworkElement;
                root.DataContext = selectedItem;
                Global.GSelectedFriendId = ((FriendsStatusItems)DataContext).Id;
                Global.GSelectedFriendName = ((FriendsStatusItems)DataContext).Name;
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    DispSelectedFriendInfo();

                });

            }
        }
        private void ListBoxFriends1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxFriends_SelectionChanged(sender, e);
        }
        private void DispSelectedFriendInfo()
        {

            NavigationService.Navigate(new Uri("/Chatting/ChattingPage.xaml", UriKind.RelativeOrAbsolute));
        }
        
       

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //switch (((Pivot)sender).SelectedIndex)
            //{
            //    case 0:
            //        MessageBox.Show("All");
            //        break;
            //    case 1:
            //        MessageBox.Show("Online");
            //        break;
            //}
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                Global.isFromChatPage = false;
                NavigationService.Navigate(new Uri("/Friends/fbStatus.xaml", UriKind.RelativeOrAbsolute));
            });
        }

      
    }
}