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
		List<TrainCardViewModel> viewmodel_list = new List<TrainCardViewModel>();

        public TransportPage()
        {
            InitializeComponent();
		}

		protected override void OnAppearing()
		{
			foreach (Direction d in Enum.GetValues(typeof(Direction)))
			{
				viewmodel_list.Add(new TrainCardViewModel(d));
			}

			Train_City.BindingContext = viewmodel_list[0];
			Train_Alamein.BindingContext = viewmodel_list[1];
			Train_Belgrave.BindingContext = viewmodel_list[2];
			Train_Lilydale.BindingContext = viewmodel_list[3];
		}
	}
}
