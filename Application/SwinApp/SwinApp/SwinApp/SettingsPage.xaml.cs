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
	public partial class SettingsPage : ContentPage
	{
        bool isDarkTheme = true;
        public SettingsPage ()
		{
			InitializeComponent ();
            ButtonChangeTheme.Clicked += OnThemeButtonClicked;
        }

        private void OnThemeButtonClicked(object sender, EventArgs e) => ChangeTheme();

        void ChangeTheme()
        {

            if (isDarkTheme)
            {
                App.Current.Resources["backgroundColor"] = Color.White;
                App.Current.Resources["textColor"] = Color.Default;
                App.Current.Resources["frameCardColor"] = Color.White;
               
            }
            else
            {
                App.Current.Resources["backgroundColor"] = Color.FromHex("33302E");
                App.Current.Resources["textColor"] = Color.White;
                App.Current.Resources["frameCardColor"] = Color.FromHex("595959");
            }

            isDarkTheme = !isDarkTheme;
        }
    }
}