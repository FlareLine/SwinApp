using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;

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

    public static class SwinIO<T>
    {
        /// <summary>
        /// Read a file's contents and serialize
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static T Read(string filename)
        {
            if (File.Exists(filename))
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename));
            else
                return default(T);
        }
        /// <summary>
        /// Read a file's contents and serialize it asynchronously
        /// </summary>
        /// <param name="filename">the name of the you're reading</param>
        /// <returns></returns>
        public static async Task<T> ReadAsync(string filename) => await Task.Run(() => Read(filename));
        /// <summary>
        /// Write a file's contents as serialized JSON
        /// </summary>
        /// <param name="filename">the filename of what you're writing to</param>
        /// <param name="obj">the object you're saving</param>
        public static void Write(string filename, T obj) => File.WriteAllText(filename, JsonConvert.SerializeObject(obj));
        /// <summary>
        /// Write a file's content as serialized json asynchronously
        /// </summary>
        /// <param name="filename">the filename of what you're writing to</param>
        /// <param name="obj">the object you're saving</param>
        /// <returns></returns>
        public static async Task WriteAsync(string filename, T obj) => await Task.Run(() => Write(filename, obj));
    }
}
