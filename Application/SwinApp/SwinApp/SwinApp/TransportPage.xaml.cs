﻿using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SwinApp.Components.UI;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransportPage : ContentPage
	{

		public List<Grid> Transports = new List<Grid>();
		
        public TransportPage()
        {
            Transports.Add(new CardTransport(DirectionId.City, RouteType.Train, RouteId.City));
            Transports.Add(new CardTransport(DirectionId.Lilydale, RouteType.Train, RouteId.Lilydale));
            Transports.Add(new CardTransport(DirectionId.Belgrave, RouteType.Train, RouteId.Belgrave));
            Transports.Add(new CardTransport(DirectionId.Alamein, RouteType.Train, RouteId.Alamein));
            Transports.Add(new CardTransport(DirectionId.KewViaStKilda, RouteType.Tram, RouteId.MelbUniKewViaStKilda));
            Transports.Add(new CardTransport(DirectionId.MelbUniViaStKilda, RouteType.Tram, RouteId.MelbUniKewViaStKilda));
            InitializeComponent();
            TransportList.ItemsSource = Transports;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			RefreshTimes();
			TransportList.ItemsSource = Transports;
		}

        private void RefreshTimes()
        {
			foreach (CardTransport ct in Transports)
				ct.Update();
		}
	}
}
