using SwinApp.Library;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwinApp
{
    /// <summary>
    /// Provides a layer between the application and the PTV library
    /// </summary>
    public static class TransportLib
    {
        /// <summary>
        /// Requests the next departure for the specified route, direction, stop id and route type
        /// </summary>
        /// <param name="r">Route id</param>
        /// <param name="d">Direction id</param>
        /// <param name="s">Stop id</param>
        /// <param name="t">Route type</param>
        /// <returns>The next departure in a Departure object</returns>
		public static async Task<Departure> GetNextDeparture(RouteId r, DirectionId d, RouteType t)
        {
            StopId s = t == RouteType.Train ? StopId.GlenferrieTrain : StopId.GlenferrieTram;
            return (await PTV.RequestPTVPayloadAsync($"departures/route_type/{(int)t}/stop/{(int)s}/route/{(int)r}?direction_id={(int)d}&max_results=1")).Departures[0];
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

    /// <summary>
    /// Routes represented in user-readable format
    /// </summary>
    public enum RouteId
	{
		City = 0,
		Alamein = 1,
		Belgrave = 2,
		Lilydale = 9,
        MelbUniKewViaStKilda = 724
    }

    /// <summary>
    /// Directions represented in user-readable format
    /// </summary>
	public enum DirectionId
	{
		Alamein = 0,
		City = 1,
		Belgrave = 3,
		Lilydale = 9,
        MelbUniViaStKilda = 11,
        KewViaStKilda = 12
    }

    /// <summary>
    /// Stops represented in user-readable format
    /// </summary>
    public enum StopId
    {
        GlenferrieTrain = 1080,
        GlenferrieTram = 2923
    }
}