using System;
using System.Threading.Tasks;

namespace SwinApp.Library
{
    public class TransportCardViewModel : ViewModel
    {
        private string _time;
        private DirectionId _direction;
        private RouteType _type;

        public string Time {
            get {
                return _time;
            }
            set {
                if (value != _time)
                {
                    _time = value;
                    NotifyPropertyChanged("Time");

                }
            }
        }
        public DirectionId Direction {
            get {
                return _direction;
            }
            set {
                if (value != _direction)
                {
                    _direction = value;
                    NotifyPropertyChanged("Direction");

                }
            }
        }
        public RouteType Type {
            get {
                return _type;
            }
            set {
                if (value != _type)
                {
                    _type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        private RouteId Route { get; set; }

        /// <summary>
        /// Constructor to create a new Transport Card View Model
        /// </summary>
        /// <param name="direction">Direction id for the card</param>
        /// <param name="type"></param>
		public TransportCardViewModel(DirectionId direction, RouteType type, RouteId route)
        {
            Direction = direction;
            Type = type;
            Time = "--";

            Route = route;
        }

        /// <summary>
        /// Gets the information for the next departure
        /// </summary>
        /// <returns>The updated information in the properties of the view model</returns>
        public async Task GetDeparture()
        {
            Departure dep = await TransportLib.GetNextDeparture(Route, Direction, Type);

            int arrivalTime = 0;

            if (dep.estimated_departure_utc == null)
                arrivalTime = (int)DateTime.Parse(dep.scheduled_departure_utc).Subtract(DateTime.Now).TotalMinutes;
            else
                arrivalTime = (int)DateTime.Parse(dep.estimated_departure_utc).Subtract(DateTime.Now).TotalMinutes;

            Time = arrivalTime > 60 ? ">60 mins" : $"{arrivalTime} mins";
        }
    }
}