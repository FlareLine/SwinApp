using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwinApp.Library
{
    public interface IDashItem
    {
        string PrimaryContent { get; }
        string SecondaryContent { get; }
    }
    /// <summary>
    /// The interface for dashboard cards
    /// </summary>
    public interface IDashCard
    {
        /// <summary>
        /// The Title is the text which appears at the top of the card
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Content is the content of the card, utilsing a grid
        /// </summary>
        Grid Content { get; }
        /// <summary>
        /// Use Load to provide a standard loading method for the card
        /// </summary>
        void Load();
        /// <summary>
        /// Use Open to provide opening logic for an entire card if necessary
        /// </summary>
        void Open();
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
    /// <summary>
    /// Simple dash card for text content
    /// </summary>
    public class TextContentDashCard : ViewModel, IDashCard
    {
        private Grid _content;
        private string _title;

        public string Title => _title;

        public Grid Content => _content;

        public TextContentDashCard(string title, string textContent)
        {
            _title = title;
            _content = new Grid();
            _content.Children.Add(new Label() { Text = textContent });
            NotifyPropertyChanged("Content");
            NotifyPropertyChanged("Title");
        }

        public void Load()
        {
            
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// Dash card for weather
    /// </summary>
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
