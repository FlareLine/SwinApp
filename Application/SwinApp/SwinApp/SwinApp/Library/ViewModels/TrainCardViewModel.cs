using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwinApp.Library
{
	public class TrainCardViewModel : ViewModel
	{
		public string Line { get; set; }
		public string Time { get; set; }
		public string Platform { get; set; }

		TransportLink tl = new TransportLink();

		public TrainCardViewModel(Direction d)
		{
			Departure departure = null; //tl.GetNextDeparture((int) d).Result;


			Line = DateTime.Now.ToShortTimeString();
			//Line = Enum.GetName(typeof(Direction), departure);

			Time = DateTime.Now.ToShortDateString();
			//Time = DateTime.Parse(departure.estimated_departure_utc).ToShortTimeString();

			Platform = "Bawsda";
			//Platform = departure.platform_number;
		}
	}

	public enum Direction
	{
		City = 1,
		Alamein = 0,
		Belgrave = 2,
		Lilydale = 9
	}
}