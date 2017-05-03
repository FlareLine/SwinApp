using SwinApp.Components;
using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransportPage : ContentPage
	{
		List<TrainCardViewModel> viewModelList = new List<TrainCardViewModel>();

        public TransportPage()
        {
            InitializeComponent();
		}

		protected async override void OnAppearing()
		{
			foreach (RouteDirection d in Enum.GetValues(typeof(RouteDirection)))
				if (d.ToString().Contains("Direction"))
				{
					Enum.TryParse(d.ToString().Replace("Direction", "Route"), out RouteDirection r);
					viewModelList.Add(new TrainCardViewModel(r, d));
				}
			TrainCity.BindingContext = viewModelList[0];
			TrainAlamein.BindingContext = viewModelList[1];
			TrainBelgrave.BindingContext = viewModelList[2];
			TrainLilydale.BindingContext = viewModelList[3];
		}
	}
}
