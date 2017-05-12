using SwinApp.Components;
using SwinApp.Components.UI;
using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		public List<Grid> Trains = new List<Grid>();
		
        public TransportPage()
        {
			Trains.Add(new CardTransport(0, 1));
			Trains.Add(new CardTransport(1, 0));
			Trains.Add(new CardTransport(2, 3));
			Trains.Add(new CardTransport(9, 9));
			InitializeComponent();
			TransportList.ItemsSource = Trains;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			foreach (CardTransport card in Trains)
			{
				card.Update();
			}
		}
	}
}
