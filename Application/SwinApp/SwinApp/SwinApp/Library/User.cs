using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SwinApp.Library
{
    /// <summary>
    /// A Student class is the scope of SwinApp 
    /// </summary>
    public static class User
    {
        private static ObservableCollection<IDashCard> _dashBoardItems = new ObservableCollection<IDashCard>();

        public static ObservableCollection<IDashCard> DashBoardItems
        {
            get { return _dashBoardItems; }
        }

        private static List<BlackboardAnnouncement> _announcements = new List<BlackboardAnnouncement>();

        public static List<BlackboardAnnouncement> Announcements => _announcements;


        public static async void LoadWeather()
        {
            WeatherDashItem weatherDash = new WeatherDashItem();
            await weatherDash.LoadWeather();
            //_dashBoardItems.Add(weatherDash);
        }
        private static void LoadBlackboardAnnouncements()
        {
#if DEBUG
            _announcements.Add(new BlackboardAnnouncement()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test Blackboard Announcement",
                Body = "Welcome to Blackboard, it's pretty sweet aye? Lots of cool stuff to mess with. \n Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem  \n Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem \n Lorem ",
                Created = DateTime.Now
            });
#else
#endif
        }
        static User()
        {
            LoadWeather();
            LoadBlackboardAnnouncements();
            foreach (BlackboardAnnouncement a in Announcements)
                _dashBoardItems.Add(new BBAnnouncementCard(a));
#if DEBUG
            _dashBoardItems.Add(new TextContentDashCard("Welcome to SwinApp", "Creators of SwinApp"));
            _dashBoardItems.Add(new TextContentDashCard("Remember, learning is fun", "Creators of SwinApp"));
            _dashBoardItems.Add(new UpNextCard(new SamplePlanned("Test Event", DateTime.Now.AddMinutes(5))));
            _dashBoardItems.Add(new WeatherCard());
#endif
        }
    }
}
