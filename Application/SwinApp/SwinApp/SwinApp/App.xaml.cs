using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#dc2d27"),
                BarTextColor = Color.White
            };
		}

		protected override async void OnStart ()
		{
            await Task.Run(async () =>
            {
                if (Library.Analytics.Analytics.DELIVER_ON_STARTUP)
                    await Library.Analytics.Analytics.DeliverLogAsync();
            });
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        public static async Task NoInternetWarn()
        {
            await Current.MainPage.DisplayAlert("No Internet!", "SwinApp could not connect to the internet :(", "OK");
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
    /// <summary>
    /// A basic class for implementing INotifyProperty changed
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
