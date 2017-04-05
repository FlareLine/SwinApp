using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SwinApp.Library
{
    /// <summary>
    /// A Student class is the scope of SwinApp and should be passed in between each view
    /// </summary>
    /// <remarks>
    /// Possibly make it static/global?
    /// </remarks>
    public static class User
    {
        private static ObservableCollection<IDashItem> _dashBoardItems = new ObservableCollection<IDashItem>();

        public static ObservableCollection<IDashItem> DashBoardItems
        {
            get { return _dashBoardItems; }
        }

        public static async void LoadWeather()
        {
            WeatherDashItem weatherDash = new WeatherDashItem();
            await weatherDash.LoadWeather();
            _dashBoardItems.Add(weatherDash);
        }
    }
}
