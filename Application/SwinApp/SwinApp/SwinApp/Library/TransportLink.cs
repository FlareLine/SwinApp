using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwinApp.Library
{
    /// <summary>
    /// Provides a layer between the application and the PTV library
    /// </summary>
    public class TransportLink
    {
        /// <summary>
        /// Requests the next departure for the specified route, direction and stop id
        /// </summary>
        /// <param name="r">Route id</param>
        /// <param name="d">Direction id</param>
        /// <param name="s">Stop id</param>
        /// <returns>The next departure in a Departure object</returns>
        /// <remarks>
        /// Does not differentiate between train and trams as the three parameters in
        /// combination should be enough to determine the route type.
        /// </remarks>
		public async Task<Departure> GetNextDeparture(int r, int d, int s)
		{
            return (await PTV.RequestPTVPayloadAsync($"departures/route_type/0/stop/1080/route/{r}?direction_id={d}&max_results=1")).Departures[0];
		}
    }
}
