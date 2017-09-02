using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using SQLite;
using Xamarin.Forms;

namespace SwinApp.Library
{
    /// <summary>
    /// Provides possible types of screen orientation
    /// </summary>
    public enum Orientation
    {
        Landscape,
        Portrait
    }
    /// <summary>
    /// Holds device specific functionality
    /// </summary>
    public static class SwinDevice
    {
        /// <summary>
        /// Cross platform property for working with the orientation
        /// </summary>
        /// <param name="or"></param>
        public static Orientation Orientation
        {
            get
            {
                return DependencyService.Get<IOrientationProvider>().Current();
            }
            set
            {
                try
                {
                    if (value != SwinDevice.Orientation)
                        switch (value)
                        {
                            case Orientation.Landscape:
                                DependencyService.Get<IOrientationProvider>().Landscape();
                                break;
                            case Orientation.Portrait:
                                DependencyService.Get<IOrientationProvider>().Portrait();
                                break;
                        }
                }
                catch (Exception e)
                {
#if DEBUG
                    e.Message.Dump();
#endif
                }
            }
        }

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
            string filePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    filename);
            if (File.Exists(filePath))
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
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
        public static void Write(string filename, T obj)
        {
            string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                filename);
#if DEBUG
            filePath.Dump();
#endif
            File.WriteAllText(filePath, JsonConvert.SerializeObject(obj));
        }
        /// <summary>
        /// Write a file's content as serialized json asynchronously
        /// </summary>
        /// <param name="filename">the filename of what you're writing to</param>
        /// <param name="obj">the object you're saving</param>
        /// <returns></returns>
        public static async Task WriteAsync(string filename, T obj) => await Task.Run(() => Write(filename, obj));
    }

    /// <summary>
    /// XML Extension Methods for making my life less annoying
    /// </summary>
    public static class SwinXML
    {
        /// <summary>
        /// Easily retrieve the value of an element as a string, if no value is a defined then an attribute with the same name is returned
        /// </summary>
        /// <param name="name">The name of the element</param>
        /// <returns></returns>
        public static string ElementValue(this XElement obj, string name) => obj.Element(name)?.Value ?? obj.Attribute(name)?.Value;
    }

    /// <summary>
    /// Provides easy methods for working with SQLite.NET
    /// </summary>
    public static class SwinDB
    {
        private static string _path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "SwinApp.db");

        private static SQLiteConnection _conn = null;

        /// <summary>
        /// Lazy load a new SQLiteConnection
        /// </summary>
        /// <returns></returns>
        public static SQLiteConnection Conn
        {
            get
            {
                return _conn ?? (_conn = new SQLiteConnection(_path));
            }
        }
    }
}
