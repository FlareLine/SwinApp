using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SwinApp.Library;
using SwinApp.Droid.Notifications;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationImplementationDroid))]

namespace SwinApp.Droid
{
	[Activity (Label = "SwinApp", Icon = "@drawable/icon", Theme="@style/SplashScreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
            base.SetTheme(Resource.Style.MainTheme);

            RequestWindowFeature(WindowFeatures.NoTitle);

            TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);
			global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new SwinApp.App ());
		}
	}

}

