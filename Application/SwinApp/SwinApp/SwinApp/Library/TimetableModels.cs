﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SwinApp.Library
{
    /// <summary>
    /// Classes that implement ITimetableData are implied
    /// to model data found in the S1 Timetable Dump.
    /// </summary>
    public interface ITimetableData
    {
        /// <summary>
        /// Import the timetable XML as an implementation of 
        /// ITimetableData. This can either be from a string source
        /// or from an already parsed XDocument.
        /// </summary>
        /// <param name="data">The XML in string form</param>
        /// <param name="xdoc">The XDocument of the pre-parsed timetable data</param>
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
            ActivityType = doc.Root.ElementValue("activityType");
            Subject = new Subject();
            Subject.Import(data, doc);
            Campus = new Campus();
            Campus.Import(data, doc);
            Schedule = new Schedule();
            Schedule.Import(data, doc);
        }
    }

    public static class AllocationExtensions
    {
        /// <summary>
        /// Return a human readble variant of the ActivityType code
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ActivityTypeReadable(this Allocation obj)
        {
            if (obj != null)
            {
                string type = obj.ActivityType.ToLower();

                if (type.Contains("le"))
                    return "Lecture";
                else if (type.Contains("tu"))
                    return "Tutorial";
                else
                    return "Allocation";
            }
            else
                return "Invalid Allocation";
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

        public List<WeekDay> DaysOfWeek { get; set; }

        public Room Room { get; set; }

        public ExDate[] ExcludedDates { get; set; }

        public void Import(string data, XDocument xdoc)
        {
            XDocument doc = xdoc ?? XDocument.Parse(data);
            XElement node = xdoc.Root.Element("schedule");
            StartDate = DateTime.ParseExact(node.ElementValue("startDate"), "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-AU"));
            EndDate = DateTime.ParseExact(node.ElementValue("endDate"), "dd/MM/yyyy", CultureInfo.GetCultureInfo("en-AU"));
            StartTime = DateTime.Parse(node.ElementValue("startTime"));
            try
            {
                Duration = Int32.Parse(node.ElementValue("duration"));
            }
            catch
            {
                Duration = -1;
            }
            var tempDayList = new List<WeekDay>();
            //TODO: Figure out why I can't call this basic ass method
            var days = doc.Root.Elements("weekDay");

            foreach (var w in node.Elements("weekDay"))
            {
                WeekDay wRes = new WeekDay();
                wRes.Import(w.ToString());
                tempDayList.Add(wRes);
            }
            DaysOfWeek = tempDayList.Where(d => d.HasSchedule).ToList();
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
    public class WeekDay : ITimetableData
    {
        public string Day { get; set; }

        public bool HasSchedule { get; set; }

        public void Import(string data, XDocument xdoc = null)
        {
            XDocument doc = xdoc ?? XDocument.Parse(data);
            XElement element = doc.Root;
            Day = element.ElementValue("day");
            try
            {
                HasSchedule = Boolean.Parse(element.ElementValue("hasSchedule"));
            }
            catch
            {
                HasSchedule = false;
            }
        }
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