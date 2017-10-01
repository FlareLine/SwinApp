using System;
using Newtonsoft.Json;

namespace SwinApp.Library
{
    public class TimetabledClass : IPlanned
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }

        public string Room { get; set; }

        public int Occurences { get; set; }

        // Return how far in the future the event is
        public TimeSpan When => Time - DateTime.Now;

        public string Description { get; set; }

        [JsonIgnore]
        public string DateMonth
        {
            get
            {
                return (Time.ToString("dd/MM/yy"));
            }
        }

        [JsonIgnore]
        public string TimeOfDay
        {
            get
            {
                return (Time.ToString("HH:mm tt"));
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

        public TimetabledClass(DateTime classDateTime, string name, string room, int occurences)
        {
            Time = classDateTime;
            Name = name;
            Room = room;
            Occurences = occurences;
        }

        // part of IPlanned but not needed for Reminders, as these are stored locally and do not need to be queried from
        // a server
        public void Refresh()
        {
        }
    }
}