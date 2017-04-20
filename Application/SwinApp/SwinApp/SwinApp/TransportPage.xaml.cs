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
        public TransportPage()
        {
            InitializeComponent();

			TrainGrid.Children.Add(new TransportCard("CITY"), 0, 0);
			TrainGrid.Children.Add(new Button { Text = "To Lilydale" }, 1, 0);
			TrainGrid.Children.Add(new Button { Text = "To Belgrave" }, 0, 1);
			TrainGrid.Children.Add(new Button { Text = "To Alamein" }, 1, 1);
		}
	}
}
