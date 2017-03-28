﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SwinApp.Library
{
    /// <summary>
    /// A Standard SwinApp connection class
    /// </summary>
    public class SwinApp
    {
    }
    /// <summary>
    /// A generic API query class, needs the type provided for serialization
    /// </summary>
    /// <typeparam name="T">The type which you are referencing</typeparam>
    public static class API<T>
    {
        /// <summary>
        /// A basic GET request with support for headers
        /// </summary>
        /// <param name="endpoint">The endpoint which you are querying</param>
        /// <param name="headers">A dictionary of header values to be applied to the request, this is optional</param>
        /// <returns>A serialized JSON request as a C# object</returns>
        public static async Task<T> GetAsync(string endpoint, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (KeyValuePair<string, string> h in headers)
                        client.DefaultRequestHeaders.Add(h.Key, h.Value);
                }
                return JsonConvert.DeserializeObject<T>(await client.GetStringAsync(endpoint));
            }
        }
    }
}