using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SwinApp.Library;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WelcomePage : CarouselPage
	{
		public WelcomePage ()
		{
			InitializeComponent ();
		}

        private void OpenSwinAppClicked(object sender, EventArgs e)
        {
            UpdateSetting();
        }

        /// <summary>
        /// Set IsFirstTime and return home
        /// </summary>
        private async void UpdateSetting()
        {
            await SwinDB.ConnAsync.UpdateAsync(new AppSetting("IsFirstTime", false));
            await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            UpdateSetting();
            return true;
        }
    }
}
