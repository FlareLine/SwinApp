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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Weather : ContentPage
	{
        private WeatherConnection _conn;
		public Weather ()
		{
            _conn = new WeatherConnection();
			InitializeComponent ();
            TextWeather.Text = _conn.Weather.Current.ToString();
		}
    }
}
