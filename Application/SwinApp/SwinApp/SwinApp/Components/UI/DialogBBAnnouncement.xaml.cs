using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinApp.Library;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DialogBBAnnouncement : ContentPage
	{
        private BlackboardAnnouncement _announcement;
		public DialogBBAnnouncement (BlackboardAnnouncement announcement)
		{
			InitializeComponent();
            BindingContext = _announcement = announcement;
            ButtonExitAnnouncement.Clicked += async (send, ev) => await Navigation.PopAsync();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
