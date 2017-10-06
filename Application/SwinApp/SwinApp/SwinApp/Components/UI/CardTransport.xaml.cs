using SwinApp.Library;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;

namespace SwinApp.Components.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardTransport : Grid
	{
		public TransportCardViewModel _viewmodel;

		public CardTransport (DirectionId direction, RouteType type, RouteId route)
		{
            _viewmodel = new TransportCardViewModel(direction, type, route);
            BindingContext = _viewmodel;
            InitializeComponent ();
            Update();
        }

		public async void Update()
		{
			await _viewmodel.GetDeparture();
		}

        public string Direction => TransportLib.DirLangKey[_viewmodel.Direction].Direction;
        public string Other => TransportLib.DirLangKey[_viewmodel.Direction].Other;
        public string Time => _viewmodel.Time;
        public string Type => Enum.GetName(typeof(RouteType), _viewmodel.Type);
    }
}
