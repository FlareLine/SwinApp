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
		List<TrainCardViewModel> viewModelList = new List<TrainCardViewModel> { new TrainCardViewModel(0, 1), new TrainCardViewModel(1, 0), new TrainCardViewModel(2, 3), new TrainCardViewModel(9, 9) };
	

        public TransportPage()
        {
            InitializeComponent();
			TrainCity.BindingContext = viewModelList[0];
			TrainAlamein.BindingContext = viewModelList[1];
			TrainBelgrave.BindingContext = viewModelList[2];
			TrainLilydale.BindingContext = viewModelList[3];
		}

		public async void OnAppearing()
		{
			foreach (TrainCardViewModel vm in viewModelList)
				vm.GetDeparture();
		}
	}
}
