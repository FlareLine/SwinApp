using Android.App;
using SwinApp.Library;
using SwinApp.Droid;
using Xamarin.Forms;

namespace SwinApp.Droid.Notifications
{
    public class NotificationImplementation : INotification
    {
        public NotificationImplementation() { }
        /// <summary>
        /// Show a simple text notification, using native Android Notification APIs
        /// </summary>
        /// <param name="text"></param>
        public void ShowTextNotification(string text)
        {
            Notification.Builder builder = new Notification.Builder(Forms.Context)
                .SetContentTitle("SwinApp")
                .SetContentText(text)
                .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis() + 10000000)
                .SetSmallIcon(Resource.Drawable.ic_play_dark);

            NotificationManager notificationManager =
                Forms.Context.GetSystemService(Android.Content.Context.NotificationService) as NotificationManager;
            const int id = 0;

            notificationManager.Notify(id, builder.Build());
        }
    }
}
