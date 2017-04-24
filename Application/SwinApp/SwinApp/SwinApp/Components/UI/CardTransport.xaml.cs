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
	public partial class CardTransport : Grid
	{
		TransportViewModel tvm;

		public CardTransport (TransportViewModel t)
		{
			InitializeComponent ();

			tvm = t;
		}
	}
}
