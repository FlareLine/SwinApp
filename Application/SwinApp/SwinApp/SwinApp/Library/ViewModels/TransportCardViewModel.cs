using System;
using System.Threading.Tasks;

namespace SwinApp.Library
{
	public class TransportCardViewModel : ViewModel
	{
		private string _time;
		private string _line;
		private string _platform;

		private int Route;
		private int Direction;	

		public string Time {
			get {
				return _time;
			}
			set {
				if(_time != value)
				{
					_time = value;
					NotifyPropertyChanged("Time");
				}
			}
		}

		public string Line {
			get {
				return _line;
			}
			set {
				if (_line != value)
				{
					_line = value;
					NotifyPropertyChanged("Line");
				}
			}
		}

		public string Platform {
			get {
				return _platform;
			}
			set {
				if (_platform != value)
				{
					_platform = value;
					NotifyPropertyChanged("Platform");
				}
			}
		}

		TransportLink link = new TransportLink();

		public TransportCardViewModel(RouteID route, DirectionID direction)
		{
			Route = (int) route;
			Direction = (int) direction;
		}

		public async Task GetNextDeparture()
		{
			Departure next = await link.GetNextDeparture(Route, Direction);

			string timeToParse = next.estimated_departure_utc ?? next.scheduled_departure_utc;
			int minsLeft = (int)DateTime.Parse(timeToParse).Subtract(DateTime.Now).TotalMinutes;
			if (minsLeft > 60) Time = ">60 mins";
			else
			{
				switch (minsLeft)
				{
					case 1:
						Time = "1 min";
						break;
					case 0:
						Time = "Now!";
						break;
					default:
						Time = $"{minsLeft} mins";
						break;
				}
			}
			Line = Enum.Parse(typeof(DirectionID), next.direction_id).ToString();
			Platform = "Platform " + (next.platform_number ?? "--");
			}
	}
}