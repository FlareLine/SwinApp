using Foundation;
using SwinApp.iOS;
using UIKit;
using UserNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationImplementationIOS))]
[assembly: Xamarin.Forms.Dependency (typeof(OrientationImplementation))]

namespace SwinApp.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
			LoadApplication (new SwinApp.App ());

            // Request authorization for notifications to be used on Xamarin.iOS devices
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, error) =>
            {
                (new NotificationImplementationIOS()).ShowTextNotification("SwinApp will now send you reminder notifications");
            });

            /* This is the fix for the ListSeparator being placed after a left-side margin, however
             * I'm unable to work out how to weave it into the solution without a custom renderer
            UITableView vie = new UITableView();
            vie.CellLayoutMarginsFollowReadableWidth = false;
            */

            var result = base.FinishedLaunching (app, options);
            UITabBar.Appearance.SelectedImageTintColor = UIColor.FromRGB(220, 45, 39);
            return result;
        }
    }
}
