using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
