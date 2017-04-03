using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SwinApp.Library
{
    public static class PTV
    {
        /// <summary>
        /// Query the PTV api and return a string of 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public async static Task<string> QueryPTVAsync(string arguments)
        {
            string key = "6baf66a7-8816-4056-8e93-94c3b66ec822"; // supplied by PTV
            int developerId = 3000193; // supplied by PTV
            string url = $"/v3/{arguments}"; // the PTV api method we want
                                             // add developer id
            url = String.Format("{0}{1}devid={2}", url, url.Contains("?") ? "&" : "?", developerId);
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            // encode key
            byte[] keyBytes = encoding.GetBytes(key);
            // encode url
            byte[] urlBytes = encoding.GetBytes(url);
            byte[] tokenBytes = new System.Security.Cryptography.HMACSHA1(keyBytes).ComputeHash(urlBytes);
            var sb = new System.Text.StringBuilder();
            // convert signature to string
            Array.ForEach<byte>(tokenBytes, x => sb.Append(x.ToString("X2")));
            // add signature to url
            url = string.Format("{0}&signature={1}", url, sb.ToString());
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync($"http://timetableapi.ptv.vic.gov.au/{url}");
            }
        }
    }
}
