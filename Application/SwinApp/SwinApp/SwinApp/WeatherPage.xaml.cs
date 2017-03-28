using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SwinApp.Library;

namespace SwinApp
{
    public partial class WeatherPage : ContentPage
    {
        WeatherConnection _conn = new WeatherConnection();
        public WeatherPage()
        {
            InitializeComponent();
        }
        /*
         * Runs on the screen appearing, asynchronously we will retrieve the weather and
         * then "bind" the weather object to the TextWeather label
         */
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _conn.DownloadWeatherAsync();
            TextWeather.BindingContext = _conn.Weather;

        }
    }
}
