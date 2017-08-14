using System.Threading.Tasks;

namespace SwinApp.Library
{
    /// <summary>
    /// Provides a layer between the application and the PTV library
    /// </summary>
    public class TransportLink
    {
        /// <summary>
        /// Requests the next departure for the specified route, direction, stop id and route type
        /// </summary>
        /// <param name="r">Route id</param>
        /// <param name="d">Direction id</param>
        /// <param name="s">Stop id</param>
        /// <param name="t">Route type</param>
        /// <returns>The next departure in a Departure object</returns>
		public async Task<Departure> GetNextDeparture(int r, int d, int s, RouteType t)
		{
            return (await PTV.RequestPTVPayloadAsync($"departures/route_type/{(int) t}/stop/{s}/route/{r}?direction_id={d}&max_results=1")).Departures[0];
		}
    }

    /// <summary>
    /// Route Type enumeration used to specify a type of service
    /// </summary>
    public enum RouteType
    {
        Train = 0,
        Tram = 1
    }
}
