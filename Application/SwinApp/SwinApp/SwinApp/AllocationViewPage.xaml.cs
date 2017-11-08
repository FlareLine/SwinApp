using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SwinApp.Library;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllocationViewPage : ContentPage
	{
        private AllocationViewModel _vm;

		public AllocationViewPage (AllocationViewModel vm)
		{
			InitializeComponent ();
            BindingContext = _vm = vm;
            MapImage.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() =>
                {
                    Device.OpenUri(new Uri(_vm.MapClickUrl));
                }),
                NumberOfTapsRequired = 1
            });
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var conn = SwinDB.Conn;
            conn.CreateTable<AllocationVisitModel>();
            conn.Insert(new AllocationVisitModel()
            {
                Description = _vm.Description
            });
            //WebMap.Source = "https://www.google.com/maps/d/viewer?mid=1XWZ-gWAvWTQrkiVk_59U3GOu_8I";
        }
    }
}
