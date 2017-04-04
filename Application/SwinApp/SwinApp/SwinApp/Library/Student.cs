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
    public class Student
    {
        private ObservableCollection<IDashItem> _dashBoardItems;

        public ObservableCollection<IDashItem> DashBoardItems
        {
            get { return _dashBoardItems; }
        }

        public Student()
        {
            _dashBoardItems = new ObservableCollection<IDashItem>();
            LoadWeather();
#if DEBUG
            _dashBoardItems.Add(new SampleDashItem("Aye Kids", "Alex"));
#endif
        }
        private async void LoadWeather()
        {
            WeatherDashItem weatherDash = new WeatherDashItem();
            await weatherDash.LoadWeather();
            _dashBoardItems.Add(weatherDash);
        }
    }
}
