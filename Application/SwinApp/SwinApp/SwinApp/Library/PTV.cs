﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SwinApp.Library
{
    public static class PTV
    {
        // developer ID supplied by PTV
        static string _devID = (string)App.Current.Resources["PTV_DEV_ID"];
        // developer KEY supplied by PTV
        static string _devKey = (string)App.Current.Resources["PTV_DEV_KEY"];

        /// <summary>
        /// Query the PTV api and return an encoded URL
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns>The encoded request URL</returns>
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
                Debug.Write($"http://timetableapi.ptv.vic.gov.au{url}");
                return await client.GetStringAsync($"http://timetableapi.ptv.vic.gov.au{url}");
            }
        }
		/// <summary>
		/// Get the PTV pyaload as a C# object
		/// </summary>
		/// <param name="req"></param>
		/// <exception cref="HttpRequestException">Throws if the App can't access the internet</exception>
		/// <returns>The PTV data</returns>
		public static async Task<PTVPayload> RequestPTVPayloadAsync(string req)
        {
            try
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
            catch (HttpRequestException e)
            {
                // Couldn't retrieve transport data
                Debug.Write(e.StackTrace);
                // Return a null task object as we don't have the data yet.
                return (PTVPayload) await Task.FromResult<object>(null);
            }
        }

        /// <summary>
        /// Handle any errors regarding receiving a null routes{}
        /// This stops the program trying to put the null object into the Routes list
        /// </summary>
        static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
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
        /// <param name="d">The direction of train on route</param>
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

    /// <summary>
    /// PTVPayload object, containing all the <see cref="T:Departure"/> objects obtained from the request
    /// </summary>
    public class PTVPayload
    {
        public List<Departure> Departures { get; set; }
	}
}
