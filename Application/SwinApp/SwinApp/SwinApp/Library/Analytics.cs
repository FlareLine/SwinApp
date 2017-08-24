using System;

namespace SwinApp.Library
{
    public static class Anaytics
    {
        /// <summary>
        /// Log an event to the SQLite database on the user's device.
        /// </summary>
        /// <param name="e">The <see cref="AnalyticEvent"/> that was fired. Usually constructed via <see langword="new"/> in the method call.</param>
        public static void LogEvent(AnalyticEvent e)
        {
            string eventParams = e.DeserializeEvent('^');
            //TODO: Develop the SQLite system and be able to split the deserialized Event into relevant fields and insert into the database
            //InsertEventIntoDatabase() { }
        }
    }

    public enum ActionType
    {
        APP_START = 0,
        CLICK_LINK_ITEM = 1,
        CLICK_MENU_ITEM = 2
    }

    public class AnalyticEvent
    {
        public ActionType Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Arguments { get; set; }

        /// <summary>
        /// Creates a new AnalyticEvent which can be used to log user events
        /// </summary>
        /// <param name="action">The <see cref="ActionType"/> of the event the user performed</param>
        /// <param name="time">The time and date of the event in <see cref="DateTime"/> format</param>
        /// <param name="args">Any extra arguments or information about the event</param>
        public AnalyticEvent(ActionType action, DateTime time, string args = null)
        {
            Type = action;
            TimeStamp = time;
            Arguments = args;
        }
        
        /// <summary>
        /// Deserializes the <see cref="AnalyticEvent"/> into a single <see langword="string"/> delimited by parameter <see langword="char"/> <paramref name="d"/>.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public string DeserializeEvent(char d)
        {
            return Enum.GetName(typeof(ActionType), Type) + d + TimeStamp.ToShortDateString() + "-" + TimeStamp.ToShortTimeString() + d + Arguments;
        }
    }
}