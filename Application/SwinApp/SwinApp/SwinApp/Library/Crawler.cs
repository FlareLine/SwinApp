﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
		/// <returns></returns>
		public static object GetYearEvents(string year)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Crawls the academic calendar page to find academic calendar information and parses this data
		/// </summary>
		/// <param name="year">The year to retrieve the data from</param>
		/// <remarks>The 'year' parameter exists only for compatibility purposes</remarks>
		/// <returns>String containing months, and the dates and information about each month's events</returns>
		static object GetAndParseData(string year)
		{
			throw new NotImplementedException();
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