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
		public TrainCardViewModel _viewmodel;

		public CardTransport (int route, int direction)
		{
            _viewmodel = new TrainCardViewModel(route, direction);
            BindingContext = _viewmodel;
            InitializeComponent ();
            Update();
        }

		public async void Update()
		{
			await _viewmodel.GetDeparture();
		}

        public string Line => _viewmodel.Line;
        public string Time => _viewmodel.Time;
        public string Platform => _viewmodel.Platform;
    }
}
