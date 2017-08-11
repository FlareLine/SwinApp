using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SwinApp.Library
{
	/// <summary>
	/// A web crawler class used to get the academic calendar of a specific year
	/// </summary>
	public static class Crawler
	{

		/// <summary>
		/// Gets the events of the specified year
		/// </summary>
		/// <param name="year">The year to check for events</param>
		/// <returns>A Calendar event containing all the year's event</returns>
		public async static Task<Calendar> GetYearEvents(string year)
		{
			return ParseData(await GetData(year));
		}

		/// <summary>
		/// Crawls the academic calendar page to find academic calendar information and parses this data
		/// </summary>
		/// <param name="year">The year to retrieve the data from</param>
		/// <remarks>The 'year' parameter exists only for compatibility purposes</remarks>
		/// <returns>String containing months, and the dates and information about each month's events</returns>
		static async Task<string> GetData(string year)
		{
            using (var client = new HttpClient())
			{
				return await client.GetStringAsync($"http://www.swinburne.edu.au/student-administration/calendar/?year={year}");
			}
		}

		/// <summary>
		/// Parses the string received from the page into a readable object
		/// </summary>
		/// <param name="data">the data string</param>
		/// <returns>Parsed data string in a Calendar object</returns>
		static Calendar ParseData(string data)
		{
            return new Calendar()
            {
                Events = new List<CalendarEvent>()
                {
                    new CalendarEvent("thisisthedate", "thisisthename", data)
                }
            };
		}
	}

	public class CalendarEvent
	{
		public string date { get; set; }
		public string name { get; set; }
		public string info { get; set; }

		/// <summary>
		/// Creates a new <see cref="T:CalendarEvent"/> with the specified parameters.
		/// </summary>
		/// <param name="d">The date of the event</param>
		/// <param name="n">The name of the event</param>
		/// <param name="i">More information about the event</param>
		public CalendarEvent(string d, string n, string i)
		{
			date = d;
			name = n;
			info = i;
		}
	}

	public class Calendar
	{
		public List<CalendarEvent> Events { get; set; }
	}
}