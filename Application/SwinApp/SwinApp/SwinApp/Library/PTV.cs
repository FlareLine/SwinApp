using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SwinApp.Library
{
    public static class PTV
    {
        // developer ID supplied by PTV
        private static string _devID = (string)App.Current.Resources["PTV_DEV_ID"];
        // developer KEY supplied by PTV
        private static string _devKey = (string)App.Current.Resources["PTV_DEV_KEY"];

        /// <summary>
        /// Query the PTV api and return a string of 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public async static Task<string> GetPTVStringAsync(string req)
        {
            // base URL for API requests
            string url = $"/v3/{req}";
            // format the URL to contain our developer ID
            url = String.Format("{0}{1}devid={2}", url, url.Contains("?") ? "&" : "?", _devID);

            // create new encoding for getting bytes of strings
            ASCIIEncoding encoding = new ASCIIEncoding();
            // encode both the key and the url
            byte[] keyBytes = encoding.GetBytes(_devKey);
            byte[] urlBytes = encoding.GetBytes(url);

            // the url token for authing the ACTUAL API request
            byte[] tokenBytes = new System.Security.Cryptography.HMACSHA1(keyBytes).ComputeHash(urlBytes);

            // stringbuilder to build the token string from its bytes
            StringBuilder sb = new StringBuilder();
            // build the string with 2 hexadecimal chars each
            foreach (var x in tokenBytes)
                sb.Append(x.ToString("X2"));
            // Array.ForEach<byte>(tokenBytes, x => );

            // add this token signature to the request URL
            url = string.Format("{0}&signature={1}", url, sb.ToString());

            // make the request to the API and await a response before returning some data
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync($"http://timetableapi.ptv.vic.gov.au{url}");
            }
        }
        /// <summary>
        /// Get the PTV pyaload as a C# object
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static async Task<PTVPayload> RequestPTVPayloadAsync(string req)
        {
            string results = await GetPTVStringAsync(req);
            // list to store departures
            return JsonConvert.DeserializeObject<PTVPayload>(results, new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Error = HandleDeserializationError
            });
        }

        /// <summary>
        /// Handle any errors regarding receiving a null routes{}
        /// This stops the program trying to put the null object into the Routes list
        /// </summary>
        public static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
		{
			var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
		}
	}

    public class Departure
    {
        public string route_id { get; set; }
        public string direction_id { get; set; }
        public string scheduled_departure_utc { get; set; }
        public string estimated_departure_utc { get; set; }
        public bool at_platform { get; set; }
        public string platform_number { get; set; }

        /// <summary>
        /// Creates a new <see cref="T:Departure"/>with the specified parameters.
        /// </summary>
        /// <param name="r">The route the train is travelling on</param>
        /// <param name="d">The direction of train towards {station id}</param>
        /// <param name="s">Scheduled time of arrival in UTC</param>
        /// <param name="e">Estimated time of arrival in UTC</param>
        /// <param name="a">If the train is at platform. Very approximate</param>
        /// <param name="p">The platform the train is (arriving) at</param>
        public Departure(string r, string d, string s, string e, bool a, string p)
        {
            route_id = r;
            direction_id = d;
            scheduled_departure_utc = s;
            estimated_departure_utc = e;
            at_platform = a;
            platform_number = p;
        }
    }

    public class Route
    {
        public int route_type { get; set; }
        public int route_id { get; set; }
        public string route_name { get; set; }
        public string route_number { get; set; }

        /// <summary>
        /// Creates a new <see cref="T:Route"/>with the specified parameters.
        /// </summary>
        /// <param name="t">The route type (0 = train)</param>
        /// <param name="i">The id of the route (in the database)</param>
        /// <param name="n">The name of the route</param>
        /// <param name="m">The number of the route</param>
        public Route(int t, int i, string n, string m)
        {
            route_type = t;
            route_id = i;
            route_number = n;
            route_name = m;
        }
    }

	public class Stop
	{
		public string stop_name { get; set; }
		public int stop_id { get; set; }

		/// <summary>
		/// Creates a new <see cref="T:Stop"/>with the specified parameters.
		/// </summary>
		/// <param name="n">The stop name (e.g. 'Glenferrie')</param>
		/// <param name="i">The stop id (not number)</param>
		public Stop(string n, int i)
		{
			stop_name = n;
			stop_id = i;
		}
	}

    public class PTVPayload
    {
        public List<Departure> Departures { get; set; }
        public List<Route> Routes { get; set; }
		public List<Stop> Stops { get; set; }
	}
}
