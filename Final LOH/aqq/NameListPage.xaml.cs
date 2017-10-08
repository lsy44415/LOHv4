using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace aqq
{
    public partial class NameListPage : ContentPage
    {
        PersonManager manager3;
        FriendManager fri;
        string s;

	



        public NameListPage(string st)
        {
           
            InitializeComponent();
            fri = FriendManager.DefaultManager;
            manager3 = PersonManager.DefaultManager;
            s = st;
          
			
        }

		protected override async void OnAppearing()
		{
			indicator.IsVisible = true;
            base.OnAppearing();
            var email = Application.Current.Properties["Email"] as string;
            listview.ItemsSource = await manager3.GetPeersAsync(s,email);
			indicator.IsVisible = false;

		}


       async void Handle_Clicked(object sender, System.EventArgs e)
        {
			listview.SelectedItem = null;
            var menuItem = sender as MenuItem;
            var per = menuItem.CommandParameter as Learner;
			var name1 = Application.Current.Properties["Name"] as string;
			var email1 = Application.Current.Properties["Email"] as string;
			var phone1 = Application.Current.Properties["Phone"] as string;
            List<Friend> f1 = await fri.CheckDuplicateAsync(email1,per.Email);
            if (f1.Count > 0)
            {
                await DisplayAlert("Alert", "You have already followed this person !", "ok");
            }
            else
            {
                
                var friend = new Friend
                {


                    Email1 = email1,
                    Phone1 = phone1,
                    Name1 = name1,
                    Email2 = per.Email,
                    Phone2 = per.Phone,
                    Name2 = per.Name,
                    Agree ="null"

                };

                await fri.SaveTaskAsync(friend);
                await DisplayAlert("Notice", "Waiting for the peer to accept. Once accepted, you can sms or email the peers from 'My Contacts' icon on the home page. ", "ok");
            }
        }
    }
}
