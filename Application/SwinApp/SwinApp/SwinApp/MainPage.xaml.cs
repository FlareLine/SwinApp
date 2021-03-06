using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SwinApp.Library;
using SwinApp.Library.Analytics;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        private bool _forceHelp = false;
        public MainPage()
        {
            InitializeComponent();
            ListMenu.ItemsSource = new List<MenuItem>()
            {
                new MenuItem("Campus Map", "Find your way around")
                {
                    Page = new GoogleMapPage()
                },
                new MenuItem("Transport", "Get home easily")
                {
                    Page = new TransportPage()
                },
                new MenuItem("More from Swinburne", "Other apps and links from Swinburne")
                {
                    Page = new LinksPage()
                },
                //new MenuItem("Analytics", "View Analytics data")
                //{
                //    Page = new AnalyticsPage()
                //},
                //new MenuItem("Timetable", "View Timetable")
                //{
                //    Page = new TimetablePage()
                //},
                new MenuItem("Settings", "Change colours and other stuff I guess")
                {
                    Page = new SettingsPage()
                },
                new MenuItem("Student Offers", "Student Discounts and Other Offers")
                {
                    Page = new StudentOffersPage()
                },
                new MenuItem("Swinburne Contacts", "Handy email addresses and phone numbers")
                {
                    Page = new ContactsPage()
                }
            };
            ListMenu.ItemTapped += MenuSelection;
            ListDashboard.ItemTapped += (send, ev) => ListDashboard.SelectedItem = null;
            ListSchedule.ItemTapped += (send, ev) => ListSchedule.SelectedItem = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                // Add a separator to the iOS 'More' menu to visually separate the list items
                //ListMenu.SeparatorVisibility = SeparatorVisibility.Default;

                ToolbarItems.Add(new ToolbarItem()
                {
                    Icon = "Plus.png",
                    Command = new Command(() => ShowContextMenu(this, null))
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
                await Analytics.LogEventAsync(new AppEvent(EventType.LINK_INTERNAL, DateTime.Now, menuItem.Title));
                await Navigation.PushAsync(menuItem.Page);
            }
            ((ListView)sender).SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            SwinDevice.Orientation = Orientation.Portrait;

            base.OnAppearing();
            if (Device.OS == TargetPlatform.Android)
                NavigationPage.SetHasNavigationBar(this, false);
            // Ensure this only loads once
            if (ListSchedule.ItemsSource == null)
            {
                try
                {
                    User.LoadUserData();
                    ListDashboard.ItemsSource = User.DashBoardItems;
                    ListSchedule.ItemsSource = User.ReminderCards;
                    ListClasses.ItemsSource = User.ClassesCards;
                }
                catch (Exception e)
                {
                    User.DashBoardItems.Add(new TextContentDashCard("An Error Occurred", $"Details: {e.Message}"));
                }
            }
            SettingsPage.ApplyTheme();
            if (await IsFirstTimeAsync() || _forceHelp)
                await Navigation.PushModalAsync(new WelcomePage());
        }

        /// <summary>
        /// Check if it is the users first time opening the app asynchronously
        /// </summary>
        /// <returns></returns>
        private async Task<bool> IsFirstTimeAsync()
        {
            SwinDB.Conn.CreateTable<AppSetting>();
            var isFirstTime = await SwinDB.ConnAsync.Table<AppSetting>().Where(a => a.SettingID == "IsFirstTime").FirstOrDefaultAsync();
            if (isFirstTime == null)
                await SwinDB.ConnAsync.InsertAsync(new AppSetting("IsFirstTime", true));
            return (await SwinDB.ConnAsync.Table<AppSetting>().Where(a => a.SettingID == "IsFirstTime").FirstOrDefaultAsync()).SettingValue;
        }

        private async void ShowContextMenu(object sender, EventArgs e)
        {
            string check = await DisplayActionSheet("Add New...", "Close", (Device.OS == TargetPlatform.iOS) ? "Close" : null,
                new string[] {
                    "Reminder",
                    "Class",
                    "Test Notification"
                });
            switch (check)
            {
                case "Reminder":
                    //create new reminder through use of pop up window, then add it to the users reminders
                    //after this, re-write reminders to the json file
                    AddNewReminder();
                    break;
                case "Test Notification":
                    DependencyService.Get<INotification>().SetTimedNotification("Test Notification", new TimeSpan(0,1,00));
                    break;
                case "Class":
                    AddNewAllocation();
                    break;
            }
        }

        private async void AddNewReminder()
        {
            await Navigation.PushAsync(new NewReminderPage());
        }

        private async void AddNewAllocation()
        {
            await Navigation.PushAsync(new NewAllocationPage());
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
