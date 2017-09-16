using SwinApp.Library.Analytics;
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
	public partial class AnalyticsPage : ContentPage
	{
        List<AppEvent> events = new List<AppEvent>();

		public AnalyticsPage ()
		{
            InitializeComponent ();
            AnalyticsList.ItemsSource = events;
            ButtonClear.Clicked += ClearData;
        }

        protected override void OnAppearing()
        {
            LoadData();
        }

        private async void LoadData()
        {
            List<AppEvent> result = await Analytics.RetrieveLogAsync();
            events = result ?? new List<AppEvent>();
        }

        private async void ClearData(object sender, EventArgs e)
        {
            int result = await Analytics.ClearLogAsync();
            if (result != 0)
            {
                await DisplayAlert("Success!", "Analytics log was cleared", "close");
                return;
            } else
            {
                await DisplayAlert("Success!", "Analytics log was cleared", "close");
                return;
            } 
        }
    }
}
