using Android.App;
using SwinApp.Library;

[assembly: Xamarin.Forms.Dependency(typeof(INotification))]
namespace SwinApp.Droid
{
    public class INotificationImplementation : INotification
    {
        /// <summary>
        /// Show a simple text notification, using native Android Notification APIs
        /// </summary>
        /// <param name="text"></param>
        public void ShowTextNotification(string text)
        {
            Notification.Builder builder = new Notification.Builder(Application.Context)
                .SetContentTitle("SwinApp")
                .SetContentText(text)
                .SetSmallIcon(Resource.Drawable.ic_play_dark);

            Notification notification = builder.Build();

            NotificationManager notificationManager =
                Application.Context.GetSystemService(Android.Content.Context.NotificationService) as NotificationManager;
            const int id = 0;

            notificationManager.Notify(id, notification);
        }
    }
}