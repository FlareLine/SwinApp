using Android.App;
using SwinApp.Library;
using SwinApp.Droid;
using Xamarin.Forms;
using System;
using Android.Content;

namespace SwinApp.Droid.Notifications
{
    public class NotificationImplementationDroid : INotification
    {
        public NotificationImplementationDroid() { }

        public void SetTimedNotification(string text, TimeSpan when)
        {
            Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("text", text);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);

            alarmManager.Set(AlarmType.RtcWakeup, DateTime.Now.Millisecond + (long)when.TotalMilliseconds, pendingIntent);
        }

        /// <summary>
        /// Show a simple text notification, using native Android Notification APIs
        /// </summary>
        /// <param name="text">Text to show in notification</param>
        public void ShowTextNotification(string text)
        {
            Notification.Builder builder = new Notification.Builder(Forms.Context)
                .SetContentTitle("SwinApp")
                .SetContentText(text)
                .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                .SetSmallIcon(Resource.Drawable.ic_play_dark);

            NotificationManager notificationManager =
                Forms.Context.GetSystemService(Android.Content.Context.NotificationService) as NotificationManager;
            const int id = 0;

            notificationManager.Notify(id, builder.Build());
        }
    }

    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new [] { Intent.ActionBootCompleted }, Priority = (int)IntentFilterPriority.LowPriority)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Notification.Builder builder = new Notification.Builder(context)
                .SetContentTitle("SwinApp")
                .SetContentText(intent.GetStringExtra("text"))
                .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                .SetSmallIcon(Resource.Drawable.ic_play_dark);

            NotificationManager notificationManager =
                context.GetSystemService(Android.Content.Context.NotificationService) as NotificationManager;
            const int id = 0;

            notificationManager.Notify(id, builder.Build());
        }
    }
}
