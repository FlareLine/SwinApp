using SQLite;
using System;

namespace SwinApp.Library.Analytics
{
    /// <summary>
    /// AppEvent class used to hold information about an event such as application start, etc.
    /// </summary>
    public class AppEvent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Type { get; set; }

        public string Info { get; set; }

        public string TimeStamp { get; set; }

        /// <summary>
        /// Blank AppEvent constructor for SQLite usage
        /// </summary>
        public AppEvent()
        {

        }

        /// <summary>
        /// Creates a new <see cref="AppEvent"/> with the specified parameters
        /// </summary>
        /// <param name="t">Type of event</param>
        /// <param name="d">Event date and time</param>
        public AppEvent(EventType t, DateTime d) : this()
        {
            Type = Enum.GetName(typeof(EventType), t);
            TimeStamp = d.ToShortDateString() + "." + d.ToShortTimeString();
        }

        /// <summary>
        /// Creates a new <see cref="AppEvent"/> with the specified parameters
        /// </summary>
        /// <param name="t">Type of event</param>
        /// <param name="d">Event date and time</param>
        /// <param name="i">Event extra information</param>
        public AppEvent(EventType t, DateTime d, string i) : this(t, d)
        {
            Info = i;
        }

        /// <summary>
        /// Deserialize this AppEvent into a string delimited by <paramref name="d"/>
        /// </summary>
        /// <param name="d">Delimiter <see langword="char"/> to use</param>
        /// <returns></returns>
        public string Deserialize(char d)
        {
            return Enum.GetName(typeof(EventType), Type) + d + TimeStamp + d + Info;
        }

        /// <summary>
        /// Static form of <see cref="Deserialize(char)"/> for deserializing other AppEvents
        /// </summary>
        /// <param name="e"><see cref="AppEvent"/> to deserialize</param>
        /// <param name="d">Delimiter <see langword="char"/> to use</param>
        /// <returns></returns>
        public static string Deserialize(AppEvent e, char d)
        {
            return Enum.GetName(typeof(EventType), e.Type) + d + e.TimeStamp + d + e.Info;
        }
    }

    /// <summary>
    /// Event Type, used to differentiate different kinds of events
    /// </summary>
    public enum EventType
    {
        APP_START,
        APP_CLOSE,
        APP_ERROR,
        LINK_EXTERNAL,
        LINK_INTERNAL,
        REMINDER_CREATE,
        OTHER_EVENT
    }
}
