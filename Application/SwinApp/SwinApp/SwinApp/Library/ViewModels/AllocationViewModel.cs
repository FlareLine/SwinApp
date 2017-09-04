using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SwinApp.Library
{
    /// <summary>
    /// Used for linking Allocation data to a timetable easily
    /// </summary>
    public class AllocationViewModel : ViewModel
    {
        private Allocation _allocation;

        public string Description => _allocation.Subject.Description ?? "Invalid Allocation";

        public string Code => _allocation.Subject.Code ?? "Invalid Allocation";

        public string TimeOfDay => _allocation.Schedule.StartTime.ToString("hh:mm tt");

        public string When => $"{Day}, {TimeOfDay}";

        public string Room => _allocation.Schedule.Room.Code.Replace("HAW_", "");

        public string Type => _allocation.ActivityTypeReadable();

        public string Day => _allocation.DayOfWeek();

        public string DayShortened => Day.Substring(0, 3);

        public string Summary => $"{Type} in {Room}";

        public string MapSource => MapsLib.GetStaticMapURL(System.Text.RegularExpressions.Regex.Replace(Room, "[0-9]", ""));

        public Xamarin.Forms.Color Color
        {
            get
            {
                int colorIndex = new Random().Next(0, 4);
                Xamarin.Forms.Color colorHex = (Xamarin.Forms.Color)Application.Current.Resources[$"TimeColor{colorIndex}"];
                return colorHex;
            }
        }

        public AllocationViewModel(Allocation allocation)
        {
            _allocation = allocation;
        }
    }
}
