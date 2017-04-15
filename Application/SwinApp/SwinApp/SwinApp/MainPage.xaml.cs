using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SwinApp.Library;

namespace SwinApp
{
	public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			InitializeComponent();
            ListDashboard.ItemsSource = User.DashBoardItems;
            ListMenu.ItemsSource = new List<MenuItem>()
            {
                new MenuItem("Timetable", "See your classes"),
                new MenuItem("Campus", "Find your way around"),
                new MenuItem("Transport", "Get home easily")
            };
            
            /*
            var viewTransportGesture = new TapGestureRecognizer();
            viewTransportGesture.Tapped += ViewTransportPage;
            ListMenu.Item[2].GestureRecognizers.Add(viewTransportGesture);
            */
		}
        //private async void ViewTransportPage(object sender, EventArgs e)
        //{
        //    await Application.Current.MainPage.Navigation.PushAsync(new TransportPage());
        //}

        protected override void OnAppearing()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            base.OnAppearing();
        }

        private async void ShowContextMenu(object sender, EventArgs e) => await DisplayActionSheet("Add New...", "Close", "", new string[] { "Reminder" });

        private void AssertPlusVisibility(object sender, ScrolledEventArgs e) => ButtonAndroidPlus.IsVisible = ScrollFeed.ScrollY > 0 ? false : true;
    }
    public class MenuItem
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public Page DestPage { get; set; }
        public MenuItem(string t, string d)
        {
            Title = t;
            Desc = d;
            DestPage = null;
        }
        public MenuItem(string t, string d, Page p)
        {
            Title = t;
            Desc = d;
            DestPage = p;
        }
    }
}
