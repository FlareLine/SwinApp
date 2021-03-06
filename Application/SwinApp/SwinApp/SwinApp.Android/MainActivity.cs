using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

using SwinApp.Droid.Notifications;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationImplementationDroid))]
[assembly: Xamarin.Forms.Dependency(typeof(OrientationImplimentaion))]

namespace SwinApp.Droid
{
    [Activity (Label = "SwinApp", Icon = "@drawable/icon", Theme="@style/SplashScreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
            base.SetTheme(Resource.Style.MainTheme);

            RequestWindowFeature(WindowFeatures.NoTitle);

            Window.SetSoftInputMode(SoftInput.AdjustPan);

            TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);
			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new SwinApp.App ());

            // HockeyApp Integration, remove for store builds
            MetricsManager.Register(Application, (string)Xamarin.Forms.Application.Current.Resources["HockeySDKAndroidID"]);
            UpdateManager.Register(this, (string)Xamarin.Forms.Application.Current.Resources["HockeySDKAndroidID"]);
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);

        }

        protected override void OnResume()
        {
            base.OnResume();
            // HockeyApp Integration, remove for store builds
            CrashManager.Register(this, (string)Xamarin.Forms.Application.Current.Resources["HockeySDKAndroidID"]);
        }

        protected override void OnPause()
        {
            base.OnPause();
            // HockeyApp Integration, remove for store builds
            UpdateManager.Unregister();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            // HockeyApp Integration, remove for store builds
            UpdateManager.Unregister();
        }
    }

}