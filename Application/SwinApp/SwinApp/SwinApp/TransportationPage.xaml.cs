using SwinApp.Components.UI;
using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransportationPage : ContentPage
	{

		public ObservableCollection<Grid> TrainCollection = new ObservableCollection<Grid>();

		public TransportationPage ()
		{
			TrainCollection.Add(new TransportCard(RouteID.City, DirectionID.City));
			TrainCollection.Add(new TransportCard(RouteID.Belgrave, DirectionID.Belgrave));
			TrainCollection.Add(new TransportCard(RouteID.Lilydale, DirectionID.Lilydale));
			TrainCollection.Add(new TransportCard(RouteID.Alamein, DirectionID.Alamein));
			InitializeComponent ();
			TrainList.ItemsSource = TrainCollection;
			Device.StartTimer(new TimeSpan(0,0,5), () =>
			{
				Task.Run(() =>
				{
					foreach (TransportCard card in TrainCollection)
					{
						card.UpdateTime();
					}
				});
				return true;
			});
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			TrainCollection = new ObservableCollection<Grid>
			{
				new TransportCard(RouteID.City, DirectionID.City),
				new TransportCard(RouteID.Belgrave, DirectionID.Belgrave),
				new TransportCard(RouteID.Lilydale, DirectionID.Lilydale),
				new TransportCard(RouteID.Alamein, DirectionID.Alamein)
			};
			TrainList.ItemsSource = TrainCollection;
		}
	}
}