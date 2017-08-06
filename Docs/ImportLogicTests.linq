<Query Kind="Program" />

void Main()
{
	string testData = @"<allocation>
        <subject>
            <code>CVE40006</code>
            <description>Infrastructure Design &amp; Project</description>
        </subject>
        <campus code=""HAW"" />
        <activityType>LE1</activityType>
        <activityCode>01</activityCode>
        <staff/>
        <schedule>
            <startDate>04/08/2014</startDate>
            <endDate>27/10/2014</endDate>
            <startTime>12:30</startTime>
            <duration>180</duration>
            <daysOfWeek>
                <weekDay day=""Monday"" hasSchedule=""true""/>
                <weekDay day=""Tuesday"" hasSchedule=""false""/>
                <weekDay day=""Wednesday"" hasSchedule=""false""/>
                <weekDay day=""Thursday"" hasSchedule=""false""/>
                <weekDay day=""Friday"" hasSchedule=""false""/>
                <weekDay day=""Saturday"" hasSchedule=""false""/>
                <weekDay day=""Sunday"" hasSchedule=""false""/>
            </daysOfWeek>
            <room code=""HAW_EN413"" />
            <excludedDates>
                <exDate start=""15/09/2014"" end=""15/09/2014"" />
            </excludedDates>
        </schedule>
    </allocation>";
	Allocation testAlloc = new Allocation();
	testAlloc.Import(testData);
	testAlloc.Dump();
	}
// Define other methods and classes here
    /// <summary>
    /// The standard element of a Timetable payload, is basically the allocated session
    /// such as a tutorial, lecture, workshop etc...
    /// </summary>
    public class Allocation
    {
        public Subject Subject { get; set; }

        public Campus Campus { get; set; }

        public string ActivityType { get; set; }

        public string ActivityCode { get; set; }

        public Staff Staff { get; set; }

        public Schedule Schedule { get; set; }
		
		public void Import(string data)
        {
            XDocument doc = XDocument.Parse(data);
            ActivityCode = doc.Root.Element("activityCode").Value;
        }
    }

    /// <summary>
    /// The subject details of an allocation, including code and description
    /// </summary>
    public class Subject
    {
        public string Code { get; set; }

        public string Description { get; set; }
    }

    /// <summary>
    /// Campus details such as the code
    /// </summary>
    public class Campus
    {
        public string Code { get; set; }
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
    public class Schedule
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public WeekDay[] DaysOfWeek { get; set; }

        public Room Room { get; set; }

        public ExDate[] ExcludedDates { get; set; }
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