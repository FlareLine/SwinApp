using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SwinApp.Library
{
    public class WeatherModel
    {
        public Dictionary<string, double> Main { get; set; }

        /**
         * Anything without JsonIgnore are properties that don't exist in the JSON we are downloading
         * This ensures that Json which is changed 
         */
        [JsonIgnore]
        public int Min => Main != null && Main.ContainsKey("temp_min") ? (int)Main["temp_min"] : -1;

        [JsonIgnore]
        public int Max => Main != null && Main.ContainsKey("temp_max") ? (int)Main["temp_max"] : -1;

        [JsonIgnore]
        public int Current => Main != null && Main.ContainsKey("temp") ? (int)Main["temp"] : -1;

        [JsonIgnore]
        public string Description => Main != null ? $"Currently {Current}°C \n(Max. {Max}°C)" : "Loading Weather...";
    }
    public class WeatherConnection
    {
        // Do not do this in future, not a great idea
        const string API_KEY = "f91384009591225adb5f7b65358e3ea6";
        // The formatted endpoint which we reference
        private string _endpoint = $@"http://api.openweathermap.org/data/2.5/weather?q=Hawthorn,au&units=metric&APPID={API_KEY}";

        private WeatherModel _weather = new WeatherModel();

        public WeatherModel Weather => _weather;

        public WeatherConnection()
        {
        }
        /**
         * Using the API class, we download the weather data from the API using 
         * the key and endpoint
         */
        public async Task DownloadWeatherAsync()
        {
            try
            {
                _weather = await API<WeatherModel>.GetAsync(_endpoint);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
