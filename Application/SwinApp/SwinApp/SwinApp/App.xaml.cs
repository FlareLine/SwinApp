using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SwinApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new SwinApp.MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
    /// <summary>
    /// A set of Debugging tools for making development easier
    /// Note that these tools they shouldn't be "usable" unless debugging
    /// </summary>
    public static class DebuggingTools
    {
        /// <summary>
        /// A simple extension method for dumping the JSON of an object to the 
        /// debugging console.
        /// </summary>
        /// <example>
        /// ExampleObject example = new ExampleObject();
        /// example.Dump();
        /// </example>
        public static void Dump(this object obj)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(obj), Formatting.Indented);
#endif
        }
    }

}
