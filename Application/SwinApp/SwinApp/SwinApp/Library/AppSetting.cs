using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwinApp.Library
{
    /// <summary>
    /// Simple model for holding boolean setting values in SwinApp
    /// </summary>
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
