using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SwinApp.Library
{
    public class Reminder : IPlanned
    {
        public string Name { get; set; }

        public DateTime Time { get; set; }

        //return how far in the future the event is
        public TimeSpan When => Time - DateTime.Now;

        public string Description { get; set; }

        [JsonIgnore]
        public string DateMonth
        {
            get
            {
                return (Time.ToShortDateString());
            }
        }

        [JsonIgnore]
        public string TimeOfDay
        {
            get
            {
                return (Time.ToShortTimeString());
            }
        }

        public Reminder(DateTime reminderDateTime, string title, string description)
        {
            Time = reminderDateTime;
            Name = title;
            Description = description;
        }
        
        // part of IPlanned but not needed for Reminders, as these are stored locally and do not need to be queried from
        // a server
        public void Refresh()
        {
        }

        

        
    }
}
