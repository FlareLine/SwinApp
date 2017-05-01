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
			foreach (Direction d in Enum.GetValues(typeof(Direction)))
				viewModelList.Add(new TrainCardViewModel(d));
			TrainCity.BindingContext = viewModelList[0];
			TrainAlamein.BindingContext = viewModelList[1];
			TrainBelgrave.BindingContext = viewModelList[2];
			TrainLilydale.BindingContext = viewModelList[3];
		}
	}
}
