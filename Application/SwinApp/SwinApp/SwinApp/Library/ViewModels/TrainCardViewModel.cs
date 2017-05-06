using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SwinApp.Library
{
	public class TrainCardViewModel : ViewModel, INotifyPropertyChanged
	{
		private string _line;
		private string _time;
		private string _platform;

		public string Line
		{
			get
			{
				return _line;
			}
			set
			{
				if (value != _line)
				{
					_line = value;
					NotifyPropertyChanged();
				}
			}
		}
		public string Time
		{
			get
			{
				return _time;
			}
			set
			{
				if (value != _time)
				{
					_time = value;
					NotifyPropertyChanged();
				}
			}
		}
		public string Platform
		{
			get
			{
				return _platform;
			}
			set
			{
				if (value != _platform)
				{
					_platform = value;
					NotifyPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		TransportLink tl = new TransportLink();

		public TrainCardViewModel(int r, int d)
		{
			GetDeparture(r, d);
			
			//Line = Enum.GetName(typeof(Direction), d);

			//Time = DateTime.Now.ToShortDateString();
			//Time = DateTime.Parse(departure.estimated_departure_utc).ToShortTimeString();

			//Platform = "Bawsda";
			//Platform = departure.platform_number;
		}

		public async void GetDeparture(int r, int d)
		{
			Departure dep = await tl.GetNextDeparture(r, d);

			int arrivalTime = 0;
			if (dep.estimated_departure_utc == null)
				arrivalTime = (int) DateTime.Parse(dep.scheduled_departure_utc).Subtract(DateTime.Now).TotalMinutes;
			else
				arrivalTime = (int) DateTime.Parse(dep.estimated_departure_utc).Subtract(DateTime.Now).TotalMinutes;

			string wholeTime = "";

			if (arrivalTime > 60)
			{
				int hours = arrivalTime / 60;
				int mins = arrivalTime % 60;
				wholeTime = $"{hours}h {mins}m";
			} else
			{
				wholeTime = $"{arrivalTime} mins";
			}

			Line = Enum.Parse(typeof(Direction), dep.direction_id).ToString();
			Time = wholeTime;
			Platform = "Platform " + (dep.platform_number ?? "--");

			Console.WriteLine($"XX {Line}, {Platform}, {Time} XX");
		}

		private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}

	public enum Direction
	{
		Alamein = 0,
		City = 1,
		Belgrave = 3,
		Lilydale = 9
	}
}