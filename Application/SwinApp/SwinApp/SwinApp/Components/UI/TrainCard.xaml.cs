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
	public partial class TrainCard : Grid
	{
		TrainCardViewModel vm;
		public TrainCard(TrainCardViewModel viewmodel)
		{
			vm = viewmodel;
			BindingContext = vm;
		}

		public void Load()
		{

		}
	}
}
