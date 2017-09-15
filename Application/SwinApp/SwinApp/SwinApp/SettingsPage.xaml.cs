using SQLite;
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
	public partial class SettingsPage : ContentPage
	{
        static bool useDarkTheme;

       
        
        public SettingsPage ()
		{
			InitializeComponent ();
            
           

            SwinDB.Conn.CreateTable<AppSetting>();

            try
            {
                
                useDarkTheme = SwinDB.Conn.Table<AppSetting>().Where(a => a.SettingID == "DarkTheme").FirstOrDefault().SettingValue;
            }
            catch (Exception E)
            {
                useDarkTheme = false;
                SwinDB.Conn.Insert(new AppSetting("DarkTheme", useDarkTheme));
            }

            SwitchChangeTheme.IsToggled = useDarkTheme;

            SwitchChangeTheme.Toggled += OnThemeButtonClicked;


        }

        private void OnThemeButtonClicked(object sender, EventArgs e) => ChangeTheme();

        void ChangeTheme()
        {

            useDarkTheme = !useDarkTheme;

            AppSetting newThemeSetting = SwinDB.Conn.Table<AppSetting>().Where(a => a.SettingID == "DarkTheme").FirstOrDefault();

            newThemeSetting.SettingValue = useDarkTheme;

            SwinDB.Conn.Update(newThemeSetting);

            ApplyTheme();

            SwitchChangeTheme.Toggled -= OnThemeButtonClicked;
            SwitchChangeTheme.IsToggled = useDarkTheme;
            SwitchChangeTheme.Toggled += OnThemeButtonClicked;

        }

        public static void ApplyTheme()
        {

            if (useDarkTheme)
            {
                App.Current.Resources["backgroundColor"] = Color.FromHex("33302E");
                App.Current.Resources["textColor"] = Color.White;
                App.Current.Resources["frameCardColor"] = Color.FromHex("595959");
            }
            else
            {
                App.Current.Resources["backgroundColor"] = Color.White;
                App.Current.Resources["textColor"] = Color.Default;
                App.Current.Resources["frameCardColor"] = Color.White;
            }
        }

        public class AppSetting
        {
            [PrimaryKey]
            public string SettingID { get; set; }

            public bool SettingValue { get; set; }

            public AppSetting(string ID, bool value)
            {
                SettingID = ID;
                SettingValue = value;
            }

            public AppSetting() { }
        }
    }
}