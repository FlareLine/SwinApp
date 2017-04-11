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

        public static async void LoadWeather()
        {
            WeatherDashItem weatherDash = new WeatherDashItem();
            await weatherDash.LoadWeather();
            //_dashBoardItems.Add(weatherDash);
        }
        static User()
        {
            LoadWeather();
#if DEBUG
            _dashBoardItems.Add(new TextContentDashCard("Welcome to SwinApp", "Creators of SwinApp"));
            _dashBoardItems.Add(new TextContentDashCard("Remember, learning is fun", "Creators of SwinApp"));
            _dashBoardItems.Add(new UpNextCard(new SamplePlanned("Test Event", DateTime.Now.AddMinutes(5))));
            _dashBoardItems.Add(new WeatherCard());
#endif
        }
    }
}
