using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

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
        static User()
        {
            _dashBoardItems.Add(new TextContentDashCard("Welcome to SwinApp", "Creators of SwinApp"));
            LoadBlackboardAnnouncements();
            LoadBlackboardUnits();
            foreach (BlackboardAnnouncement a in Announcements)
                _dashBoardItems.Add(new BBAnnouncementCard(a));
            if (USE_PROTOTYPE_DATA)
            {
                _dashBoardItems.Add(new TextContentDashCard("Remember, learning is fun", "Creators of SwinApp"));
                _dashBoardItems.Add(new UpNextCard(new SamplePlanned("Test Event", DateTime.Now.AddMinutes(5))));
                _dashBoardItems.Add(new WeatherCard());
            }
        }
    }
}
