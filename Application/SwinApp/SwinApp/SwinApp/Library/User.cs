using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;

namespace SwinApp.Library
{
    /// <summary>
    /// A Student class is the scope of SwinApp 
    /// </summary>
    public static class User
    {
        /// <summary>
        /// Used to allow for prototype dummy data
        /// </summary>
        public const bool USE_PROTOTYPE_DATA = true;

        private static ObservableCollection<IDashCard> _dashBoardItems = new ObservableCollection<IDashCard>();

        public static ObservableCollection<IDashCard> DashBoardItems
        {
            get { return _dashBoardItems; }
        }

        private static List<BlackboardAnnouncement> _announcements = new List<BlackboardAnnouncement>();

        public static List<BlackboardAnnouncement> Announcements => _announcements;

        private static List<BlackboardUnit> _units = new List<BlackboardUnit>();

        public static List<BlackboardUnit> Units => _units;

        public static Dictionary<string, string> UnitPairs => _units.ToDictionary(u => u.Name, u => u.UUID);

        public static async Task AddDashCard(IDashCard card)
        {
            await Task.Run(() => _dashBoardItems.Add(card));
        }
        private static void LoadBlackboardAnnouncements()
        {
            _announcements = new List<BlackboardAnnouncement>();
            if (USE_PROTOTYPE_DATA)
            {
                _announcements.Add(new BlackboardAnnouncement()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Test Blackboard Announcement",
                    Body = "Welcome to Blackboard, it's pretty sweet aye? Lots of cool stuff to mess with. \n Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem  \n Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem \n Lorem ",
                    Created = DateTime.Now
                });
            }
        }
        private static void LoadBlackboardUnits()
        {
            _units = new List<BlackboardUnit>();
            if (USE_PROTOTYPE_DATA)
            {
                _units.Add(new BlackboardUnit()
                {
                    Name = "Test Unit",
                    Id = new Random().Next(100).ToString(),
                    UUID = Guid.NewGuid().ToString(),
                });
            }
        }
        public static void LoadUserData()
        {
            ClearDashItemsSafe();
            AddDashItemSafe(new TextContentDashCard("Welcome to SwinApp", "Creators of SwinApp"));
            LoadBlackboardAnnouncements();
            LoadBlackboardUnits();
            foreach (BlackboardAnnouncement a in Announcements)
                AddDashItemSafe(new BBAnnouncementCard(a));
            if (USE_PROTOTYPE_DATA)
            {
                AddDashItemSafe(new TextContentDashCard("Remember, learning is fun", "Creators of SwinApp"));
                AddDashItemSafe(new UpNextCard(new SamplePlanned("Test Event", DateTime.Now.AddMinutes(5))));
                AddDashItemSafe(new WeatherCard());
            }
        }
        /// <summary>
        /// Safely clear the dashitems of all its contents
        /// </summary>
        private static void ClearDashItemsSafe() => Device.BeginInvokeOnMainThread(() => _dashBoardItems.Clear());
        /// <summary>
        /// Safely add DashItem when using asynchronous threads
        /// </summary>
        /// <param name="card"></param>
        public static void AddDashItemSafe(IDashCard card) => Device.BeginInvokeOnMainThread(() => _dashBoardItems.Add(card));

        static User()
        {
        }
    }
}
