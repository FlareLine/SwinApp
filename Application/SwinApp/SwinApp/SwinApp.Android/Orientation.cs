using Android.App;
using Xamarin.Forms;

using SwinApp.Library;

namespace SwinApp.Droid.Notifications
{
    public class OrientationImplimentaion : IOrientationProvider
    {
        public Orientation Current()
        {
            switch (((Activity)Forms.Context).RequestedOrientation)
            {
                case Android.Content.PM.ScreenOrientation.Landscape:
                case Android.Content.PM.ScreenOrientation.ReverseLandscape:
                    return Orientation.Landscape;
                default:
                    return Orientation.Portrait;
            }
        }

        public void Landscape() => ((Activity)Forms.Context).RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;

        public void Portrait() => ((Activity)Forms.Context).RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
    }
}
