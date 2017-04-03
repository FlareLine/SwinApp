using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SwinApp.Library;

namespace SwinApp
{
	public partial class MainPage : TabbedPage
	{
        private WeatherDashItem _weatherDash = new WeatherDashItem();
        private List<IDashItem> _items = new List<IDashItem>()
        {
            new SampleDashItem("Hello\n friends", "Alex"),
            new SampleDashItem("It is I,\n Alex", "Ethan")
        };
		public MainPage()
		{
			InitializeComponent();
            ListDashboard.ItemsSource = _items;
		}
        protected async override void OnAppearing()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            await _weatherDash.LoadWeather();
            _items.Add(_weatherDash);
            base.OnAppearing();
        }
    }
}
