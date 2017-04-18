using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SwinApp.Library
{
    ///<summary>
    /// The typical Blackboard Announcment Payload
    ///</summary>
    ///<remarks>
    /// For the actual announcement class, refer to BlackboardAnnouncement
    ///</summary>
    public class BlackboardAnnouncementPayload
    {
        public List<BlackboardAnnouncement> Results { get; set; }
        public Dictionary<string, string> Paging { get; set; }
    }
    ///<summary>
    /// An announcment model from Blackboard
    ///</summary>
    public class BlackboardAnnouncement
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public BlackboardAnnouncmentAvailability Availability { get; set; }
        public bool ShowAtLogin { get; set; }
        public bool ShowInCourses { get; set; }
        public DateTime Created { get; set; }
    }
    ///<summary>
    /// Availability details about a blackboard announcement
    ///</summary>    
    public class BlackboardAnnouncmentAvailability
    {
        BlackboardAnnouncementDuration Duration { get; set; }
    }
    ///<summary>
    /// The duration and type of a blackboard announcement
    ///</summary>
    public class BlackboardAnnouncementDuration
    {
        public string Type { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}