using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SwinApp.Library;
using Xamarin.Forms.Xaml;
using SwinApp.Library.Analytics;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            ListMenu.ItemsSource = new List<MenuItem>()
            {
                new MenuItem("Timetable", "See your classes")
                {
                    Page = new TimetablePage()
                },
                new MenuItem("Campus Map", "Find your way around")
                {
                    Page = new GoogleMapPage()
                },
                new MenuItem("Transport", "Get home easily")
                {
                    Page = new TransportPage()
                },
                new MenuItem("Announcements", "Keep in the loop with all going on at Uni")
                {
                    Page = new AnnouncementPage()
                },
                new MenuItem("More from Swinburne", "Other apps and links from Swinburne")
                {
                    Page = new LinksPage()
                }
            };
            ListMenu.ItemTapped += MenuSelection;
            ListDashboard.ItemTapped += (send, ev) => ListDashboard.SelectedItem = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                ToolbarItems.Add(new ToolbarItem()
                {
                    Icon = "Plus.png",
                    Command = new Command(() => AddNewReminder())
                });
                PageMore.Title = "Menu";
                PageMore.Icon = "Menu.png";
                PageCalendar.Icon = "Calendar.png";
                PageDashboard.Icon = "Home.png";
            }
        }

        private void RefreshSchedule()
        {
            ListSchedule.BeginRefresh();
        }

        private async void MenuSelection(object sender, ItemTappedEventArgs e)
        {
            var menuItem = ListMenu.SelectedItem as MenuItem;
            if (menuItem.Page != null)
            {
                await Analytics.LogEventAsync(new AppEvent()
                {
                    TimeStamp = DateTime.Now,
                    Type = EventType.LINK_INTERNAL,
                    Info = menuItem.Title
                });
                await Navigation.PushAsync(menuItem.Page);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.OS == TargetPlatform.Android)
                NavigationPage.SetHasNavigationBar(this, false);
            try
            {
                User.LoadUserData();
                ListDashboard.ItemsSource = User.DashBoardItems;
                ListSchedule.ItemsSource = User.TimetableCards;
            }
            catch (Exception e)
            {
                User.DashBoardItems.Add(new TextContentDashCard("An Error Occurred", $"Details: {e.Message}"));
            }
        }

        private async void ShowContextMenu(object sender, EventArgs e)
        {
            string check = await DisplayActionSheet("Add New...", "Close", "", new string[] { "Reminder" });
            if (check == "Reminder")
            {
                //create new reminder through use of pop up window, then add it to the users reminders
                //after this, re-write reminders to the json file
                AddNewReminder();

            }
        }

        private async void AddNewReminder()
        {
            await Navigation.PushAsync(new NewReminderPage());
        }

        private void AssertPlusVisibility(object sender, ScrolledEventArgs e)
        {
            ButtonAndroidPlusFeed.IsVisible = ScrollFeed.ScrollY > 0 ? false : true;
            ButtonAndroidPlusSchedule.IsVisible = ScrollFeed.ScrollY > 0 ? false : true;
        }

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