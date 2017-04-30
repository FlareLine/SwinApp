﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SwinApp.Library
{
    class TransportLink
    {
		public async Task<Departure> GetNextDeparture(int d)
		{
			return (await PTV.RequestPTVPayloadAsync($"departures/route_type/0/stop/1080/route/0?direction_id={d}&max_results=1")).Departures[0];
		}
    }
}
