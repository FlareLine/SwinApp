using System;
using Xamarin.Forms;
using SQLite;

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

        public Xamarin.Forms.Color Color
        {
            get
            {
                // Create table if not exists
                var conn = SwinDB.Conn;
                conn.CreateTable<AllocationColour>();

                if (conn.Table<AllocationColour>().FirstOrDefault(a => a.SubjectCode == _allocation.Subject.Code) == null)
                {
                    int colorIndex = new Random().Next(0, 4);
                    string hexCode = $"TimeColor{colorIndex}";
                    Xamarin.Forms.Color colorHex = (Xamarin.Forms.Color)Application.Current.Resources[$"TimeColor{colorIndex}"];
                    return colorHex;
                }
                else
                {
                    return Color.Black;
                }
            }
        }

        public AllocationViewModel(Allocation allocation)
        {
            _allocation = allocation;
        }
    }
}
