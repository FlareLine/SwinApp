using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp.Components.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransportCard : Grid
	{
		public TransportCardViewModel _vm;

		public string Time => _vm.Time;
		public string Line => _vm.Line;
		public string Platform => _vm.Platform;

		public TransportCard(RouteID route, DirectionID direction)
		{
			_vm = new TransportCardViewModel(route, direction);
			UpdateTime();
			BindingContext = _vm;
			InitializeComponent();
		}

		public async void UpdateTime()
		{
			await _vm.GetNextDeparture();
			BindingContext = _vm;
			Debug.Write($"XXX {Line} {Time}");
		}
	}
}