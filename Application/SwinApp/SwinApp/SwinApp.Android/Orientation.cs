using Android.App;
using SwinApp.Library;
using SwinApp.Droid;
using Xamarin.Forms;

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

        public void Horizontal() => ((Activity)Forms.Context).RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;

        public void Portrait() => ((Activity)Forms.Context).RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
    }
}
