using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SwinApp.Library
{
    public class TimetabledClass : IPlanned
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string Room { get; set; }

        public int Occurences { get; set; }

        public string Type { get; set; }

        public Color classColor { 

            get
            {
                return Color.FromHex(HexColor);
            }

        }

        public string HexColor { get; set; }

        // Return how far in the future the event is
        public TimeSpan When => Time - DateTime.Now;

        public string Description { get; set; }

        [JsonIgnore]
        public string DateMonth
        {
            get
            {
                return (Time.ToString("dd/MM"));
            }
        }

        [JsonIgnore]
        public string TimeOfDay
        {
            get
            {
                return (Time.ToString("hh:mm tt"));
            }
        }

        [JsonIgnore]
        public string Day
        {
            get
            {
                return (Time.ToString("ddd"));
            }
        }

       
        public TimetabledClass(DateTime classDateTime, string name, string room, int occurences, string type, Color color)
        {
            Time = classDateTime;
            Name = name;
            Room = room;
            Occurences = occurences;
            Type = type;
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