using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp.Components.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransportGrid : Grid
	{
		public TransportGrid ()
		{
			InitializeComponent ();

			TrainCity = new TransportCard();
			TrainBelgrave = new TransportCard();
			TrainLilydale = new TransportCard();
			TrainAlamein = new TransportCard();
		}
	}
}
