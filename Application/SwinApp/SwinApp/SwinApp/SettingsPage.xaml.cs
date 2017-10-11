﻿using SQLite;
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
        public static bool use12HourTime;
        public static bool autoClassNotify;



        public SettingsPage()
        {
            InitializeComponent();



            SwinDB.Conn.CreateTable<AppSetting>();


            var darkThemeSetting = SwinDB.Conn.Table<AppSetting>().Where(a => a.SettingID == "DarkTheme").FirstOrDefault();
            useDarkTheme = darkThemeSetting != null ? darkThemeSetting.SettingValue : false;
            if (darkThemeSetting == null)
                SwinDB.Conn.Insert(new AppSetting("DarkTheme", useDarkTheme));

            var hourTimeSetting = SwinDB.Conn.Table<AppSetting>().Where(a => a.SettingID == "12HourTime").FirstOrDefault();
            use12HourTime = hourTimeSetting != null ? hourTimeSetting.SettingValue : false;
            if (hourTimeSetting == null)
                SwinDB.Conn.Insert(new AppSetting("12HourTime", use12HourTime));

			var autoNotifySetting = SwinDB.Conn.Table<AppSetting>().Where(a => a.SettingID == "AutoClassNotify").FirstOrDefault();
			autoClassNotify = autoNotifySetting != null ? autoNotifySetting.SettingValue : false;
			if (autoNotifySetting == null)
				SwinDB.Conn.Insert(new AppSetting("AutoClassNotify", autoClassNotify));

            SwitchChangeTheme.IsToggled = useDarkTheme;
            SwitchChangeTime.IsToggled = use12HourTime;
            SwitchAutoClassNotify.IsToggled = autoClassNotify;

            SwitchChangeTheme.Toggled += OnThemeButtonClicked;
            SwitchChangeTime.Toggled += OnTimeButtonClicked;
            SwitchAutoClassNotify.Toggled += OnAutoClassNotifyButtonClicked;

            var analyticsTapped = new TapGestureRecognizer();
            analyticsTapped.Tapped += OpenAnalyticsPage;
            FrameAnalytics.GestureRecognizers.Add(analyticsTapped);
        }

        private async void OpenAnalyticsPage(object sender, EventArgs e) => await Navigation.PushAsync(new AnalyticsPage());

        private void OnThemeButtonClicked(object sender, EventArgs e) => ChangeTheme();

        private void OnTimeButtonClicked(object sender, EventArgs e) => ToggleTime();

        private void OnAutoClassNotifyButtonClicked(object sender, EventArgs e) => ToggleAutoClassNotify();

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
                App.Current.Resources["iosOutlineColor"] = Color.FromHex("595959");
            }
            else
            {
                App.Current.Resources["backgroundColor"] = Color.White;
                App.Current.Resources["textColor"] = Color.Default;
                App.Current.Resources["frameCardColor"] = Color.White;
                App.Current.Resources["iosOutlineColor"] = Color.White;
            }
        }

        private void ToggleTime()
        {
            use12HourTime = !use12HourTime;

            AppSetting newTimeSetting = SwinDB.Conn.Table<AppSetting>().Where(a => a.SettingID == "Time").FirstOrDefault();

            newTimeSetting.SettingValue = useDarkTheme;

            SwinDB.Conn.Update(newTimeSetting);

            User.PopulateSchedule();

            SwitchChangeTime.Toggled -= OnTimeButtonClicked;
            SwitchChangeTime.IsToggled = use12HourTime;
            SwitchChangeTime.Toggled += OnTimeButtonClicked;
        }

        private void ToggleAutoClassNotify()
        {
            autoClassNotify = !autoClassNotify;
            /*AppSetting newAutoClassNotifySetting = SwinDB.Conn.Table<AppSetting>().Where(a => a.SettingID == "AutoClassNotify").FirstOrDefault();
            newAutoClassNotifySetting.SettingValue = autoClassNotify;
            SwinDB.Conn.Update(newAutoClassNotifySetting);*/

            SwitchAutoClassNotify.Toggled -= OnAutoClassNotifyButtonClicked;
            SwitchAutoClassNotify.IsToggled = autoClassNotify;
            SwitchAutoClassNotify.Toggled += OnAutoClassNotifyButtonClicked;
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
