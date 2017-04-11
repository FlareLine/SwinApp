using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SwinApp.Components;

namespace SwinApp.Library
{
    public class WeatherCard : IDashCard
    {
        private WeatherViewModel _weather;
        private Grid _content;
        public WeatherCard()
        {
            _weather = new WeatherViewModel();
            _content = new CardWeather(_weather);
            Load();
        }
        public string Title => "Weather";

        public Grid Content => _content;

        public async void Load()
        {
            await _weather.Load();
        }
    }
}
