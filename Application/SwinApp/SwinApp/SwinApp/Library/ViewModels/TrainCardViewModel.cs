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

		public TrainCardViewModel(RouteDirection r, RouteDirection d)
		{
			GetDeparture(r, d);
			
			//Line = Enum.GetName(typeof(RouteDirection), departure);

			//Time = DateTime.Now.ToShortDateString();
			//Time = DateTime.Parse(departure.estimated_departure_utc).ToShortTimeString();

			//Platform = "Bawsda";
			//Platform = departure.platform_number;
		}

		public async void GetDeparture(RouteDirection r, RouteDirection d)
		{
			Departure dep = await tl.GetNextDeparture((int) r, (int) d);
			Line = ((RouteDirection) int.Parse(dep.route_id)).ToString();
			string s = null;
			try
			{
				s = DateTime.Parse(dep.estimated_departure_utc).ToShortTimeString();
			} catch(Exception e)
			{
				Console.WriteLine(e.Message);
				Time = s ?? "NULLTIME";
			}
			Platform = dep.platform_number;
			Console.WriteLine("TIME: " + (s ?? "NULLTIME"));
		}
	}

	public enum RouteDirection
	{
		CityRoute = 1,
		CityDirection = 1,
		AlameinRoute = 1,
		AlameinDirection = 0,
		BelgraveRoute = 2,
		BelgraveDirection = 3,
		LilydaleRoute = 9,
		LilydaleDirection = 9
	}
}