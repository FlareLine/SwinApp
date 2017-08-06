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
#if DEBUG
            // TODO: REMOVE
            string testData = @"<allocation>
        <subject>
            <code>CVE40006</code>
            <description>Infrastructure Design &amp; Project</description>
        </subject>
        <campus code=""HAW"" />
        <activityType>LE1</activityType>
        <activityCode>01</activityCode>
        <staff/>
        <schedule>
            <startDate>04/08/2014</startDate>
            <endDate>27/10/2014</endDate>
            <startTime>12:30</startTime>
            <duration>180</duration>
            <daysOfWeek>
                <weekDay day=""Monday"" hasSchedule=""true""/>
                <weekDay day=""Tuesday"" hasSchedule=""false""/>
                <weekDay day=""Wednesday"" hasSchedule=""false""/>
                <weekDay day=""Thursday"" hasSchedule=""false""/>
                <weekDay day=""Friday"" hasSchedule=""false""/>
                <weekDay day=""Saturday"" hasSchedule=""false""/>
                <weekDay day=""Sunday"" hasSchedule=""false""/>
            </daysOfWeek>
            <room code=""HAW_EN413"" />
            <excludedDates>
                <exDate start=""15/09/2014"" end=""15/09/2014"" />
            </excludedDates>
        </schedule>
    </allocation>";

            Allocation testAllocation = new Allocation();
            testAllocation.Import(testData);
#endif
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
                await Navigation.PushAsync(menuItem.Page);
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
                ListSchedule.ItemsSource = User.ScheduleItems;
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