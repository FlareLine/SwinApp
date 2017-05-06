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
			TrainCity.BindingContext = new TrainCardViewModel(0, 1);
			TrainAlamein.BindingContext = new TrainCardViewModel(1, 0);
			TrainBelgrave.BindingContext = new TrainCardViewModel(2, 3);
			TrainLilydale.BindingContext = new TrainCardViewModel(9, 9);
		}
	}
}
