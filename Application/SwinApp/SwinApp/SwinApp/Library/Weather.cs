using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwinApp.Library
{
    public class WeatherModel
    {
        public Dictionary<string, double> Main { get; set; }

        [JsonIgnore]
        public int Min => Main.ContainsKey("temp_min") ? (int)Main["temp_min"] : -1;

        [JsonIgnore]
        public int Max => Main.ContainsKey("temp_max") ? (int)Main["temp_max"] : -1;

        [JsonIgnore]
        public int Current => Main.ContainsKey("temp") ? (int)Main["temp"] : -1;
    }
    public class WeatherConnection
    {
        const string API_KEY = "f91384009591225adb5f7b65358e3ea6";
        private string _endpoint = $@"http://api.openweathermap.org/data/2.5/weather?q=Hawthorne,au&units=metric&APPID={API_KEY}";

        private WeatherModel _weather = new WeatherModel();

        public WeatherModel Weather => _weather;

        public WeatherConnection()
        {
            DownloadWeather();
        }
        public async void DownloadWeather()
        {
            _weather = await API<WeatherModel>.GetAsync(_endpoint);
        }
    }
}
