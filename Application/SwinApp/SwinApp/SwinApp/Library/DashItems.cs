using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwinApp.Library
{
    public interface IDashItem
    {
        string PrimaryContent { get; }
        string SecondaryContent { get; }
    }
    public class SampleDashItem : IDashItem
    {
        private string _content;
        private string _author;

        public SampleDashItem(string Content, string Author)
        {
            _content = Content;
            _author = Author;
        }

        public string PrimaryContent => _content;

        public string SecondaryContent => _author;
    }
    public class WeatherDashItem : IDashItem
    {
        private WeatherConnection _conn;
        private string _weather = "";
        public WeatherDashItem()
        {
            _conn = new WeatherConnection();
        }
        public async Task LoadWeather()
        {
            await _conn.DownloadWeatherAsync();
            _weather = _conn.Weather.Description;
        }
        public string PrimaryContent => _weather;
        public string SecondaryContent => _conn.Weather.Current > 20 ? "Beautiful weather outside" : "You might need a jumper";
    }
}
