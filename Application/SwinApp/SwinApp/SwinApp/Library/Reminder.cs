using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SwinApp.Library
{
    public class Reminder : IPlanned
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }

        // Return how far in the future the event is
        public TimeSpan When => Time - DateTime.Now;

        public string Description { get; set; }

        public string HexColor { get; set; }

        public Color ReminderColor = Color.FromHex(HexColor);

        [JsonIgnore]
        public string DateMonth => (Time.ToString("dd/MM/yy"));

        [JsonIgnore]
        public string TimeOfDay => Time.ToString("hh:mm tt");

        [JsonIgnore]
        public string Day => (Time.ToString("ddd"));

        public Reminder(DateTime reminderDateTime, string title, string description)
        {
            Time = reminderDateTime;
            Name = title;
            Description = description;

            int red = (int)(color.R * 255);
            int green = (int)(color.G * 255);
            int blue = (int)(color.B * 255);
            int alpha = (int)(color.A * 255);
            HexColor = String.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", alpha, red, green, blue);
        }

        // part of IPlanned but not needed for Reminders, as these are stored locally and do not need to be queried from
        // a server
        public void Refresh()
        {
        }
    }
}
