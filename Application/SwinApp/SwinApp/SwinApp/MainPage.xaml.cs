using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SwinApp.Library;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            ListMenu.ItemsSource = new List<MenuItem>
            {
                new MenuItem("Timetable", "See your classes"),
                new MenuItem("Campus Map", "Find your way around")
                {
                    Page = new GoogleMapPage()
                },
                new MenuItem("Transport", "Get home easily"),
                new MenuItem("Announcements", "Keep in the loop with all going on at Uni")
                {
                    Page = new AnnouncementPage()
                }
            };
            ListMenu.ItemTapped += MenuSelection;
            ListDashboard.ItemsSource = User.DashBoardItems;
            ListSchedule.ItemsSource = User.ScheduleItems;
        }

        private async void MenuSelection(object sender, ItemTappedEventArgs e)
        {
            var menuItem = ListMenu.SelectedItem as MenuItem;
            if (menuItem.Page != null)
                await Navigation.PushAsync(menuItem.Page);
        }

        protected override void OnAppearing()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            base.OnAppearing();
            try
            {
                User.LoadUserData();
            }
            catch (Exception e)
            {
                User.DashBoardItems.Add(new TextContentDashCard("An Error Occurred", $"Details: {e.Message}"));
            }
        }

        private async void ShowContextMenu(object sender, EventArgs e)
        {
            await DisplayActionSheet("Add New...", "Close", "", new string[] { "Reminder" });
        }

        private void AssertPlusVisibility(object sender, ScrolledEventArgs e) => ButtonAndroidPlusFeed.IsVisible = ScrollFeed.ScrollY > 0 ? false : true;
    }
    public class MenuItem
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public Page Page { get; set; }
        public MenuItem(string t, string d)
        {
            Title = t;
            Desc = d;
        }
    }
}
