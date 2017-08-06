﻿using System;
using System.Linq;
using System.Xml.Linq;

namespace SwinApp.Library
{

    public interface ITimetableData
    {
        void Import(string data, XDocument xdoc = null);
    }

    /// <summary>
    /// The standard element of a Timetable payload, is basically the allocated session
    /// such as a tutorial, lecture, workshop etc...
    /// </summary>
    public class Allocation : ITimetableData
    {
        public Subject Subject { get; set; }

        public Campus Campus { get; set; }

        public string ActivityType { get; set; }

        public string ActivityCode { get; set; }

        public Staff Staff { get; set; }

        public Schedule Schedule { get; set; }

        public void Import(string data, XDocument xdoc = null)
        {
            XDocument doc = xdoc ?? XDocument.Parse(data);
            ActivityCode = doc.Root.ElementValue("activityCode");
            Subject = new Subject();
            Subject.Import(data, doc);
            Campus = new Campus();
            Campus.Import(data, doc);
        }
    }

    /// <summary>
    /// The subject details of an allocation, including code and description
    /// </summary>
    public class Subject : ITimetableData
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public void Import(string data, XDocument xdoc = null)
        {
            XDocument doc = xdoc ?? XDocument.Parse(data);
            XElement node = doc.Root.Element("subject");
            Code = node.ElementValue("code");
            Description = node.ElementValue("description");
        }
    }

    /// <summary>
    /// Campus details such as the code
    /// </summary>
    public class Campus : ITimetableData
    {
        public string Code { get; set; }

        public void Import(string data, XDocument xdoc)
        {
            XDocument doc = xdoc ?? XDocument.Parse(data);
            Code = doc.Root.Element("campus").ElementValue("code");
        }
    }

    /// <summary>
    /// Staff details of an allocation
    /// </summary>
    /// <remarks>
    /// Not sure as to what data is kept in this field but is mapped for consistency sake
    /// </remarks>
    public class Staff
    {

    }

    /// <summary>
    /// Schedule contains data relating to the time of an allocation such as the start/end time,
    /// allocated day/s, duration and excluded dates
    /// </summary>
    public class Schedule : ITimetableData
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public WeekDay[] DaysOfWeek { get; set; }

        public Room Room { get; set; }

        public ExDate[] ExcludedDates { get; set; }

        public void Import(string data, XDocument xdoc)
        {
            XDocument doc = xdoc ?? XDocument.Parse(data);
            XElement node = xdoc.Element("schedule");
            StartDate = DateTime.Parse(node.ElementValue("startDate"));
            EndDate = DateTime.Parse(node.ElementValue("endDate"));
            StartTime = DateTime.Parse(node.ElementValue("startTime"));
            try
            {
                Duration = Int32.Parse(node.ElementValue("duration"));
            }
            catch
            {
                Duration = -1;
            }
            // TODO: Properly Implement
            DaysOfWeek = new WeekDay[7];
            ExcludedDates = new ExDate[0];

            Room = new Room()
            {
                Code = node.Element("room").ElementValue("code")
            };
        }
    }

    /// <summary>
    /// Used by schedule, WeekDay is used to note which day of the week the allocation takes
    /// place on.
    /// </summary>
    public class WeekDay
    {
        public string Day { get; set; }

        public bool HasSchedule { get; set; }
    }

    /// <summary>
    /// The room details of the allocation such as the code
    /// </summary>
    public class Room
    {
        public string Code { get; set; }
    }

    /// <summary>
    /// The excluded date details for an allocation
    /// </summary>
    public class ExDate
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
