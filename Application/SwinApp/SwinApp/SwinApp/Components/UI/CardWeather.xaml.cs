using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SwinApp.Library;

namespace SwinApp.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    partial class CardWeather : Grid
	{
        WeatherViewModel vm;
		public CardWeather (WeatherViewModel viewmodel)
		{
			InitializeComponent();
            BindingContext = vm = viewmodel;
		}
	}
}
