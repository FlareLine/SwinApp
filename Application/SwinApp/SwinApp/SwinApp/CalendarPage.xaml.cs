using SwinApp.Library;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalendarPage : ContentPage
	{

        public string CalendarData { get; set; }

        public CalendarPage ()
		{
            RetrieveData();
			InitializeComponent ();
		}

        async void RetrieveData()
        {
            CalendarData = (await Crawler.GetYearEvents("2017")).Events.First().info;
            Debug.Write(CalendarData);
        }
	}
}