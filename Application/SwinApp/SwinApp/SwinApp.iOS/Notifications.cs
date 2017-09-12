using SwinApp.Library;
using UIKit;
using UserNotifications;

namespace SwinApp.iOS
{
    public class NotificationImplementationIOS : INotification
    {
		/// <summary>
		/// Show a simple text notification, using native iOS Notification APIs
		/// </summary>
		/// <param name="text">Text to show in notification</param>
		public void ShowTextNotification(string text)
        {
            UNMutableNotificationContent content = new UNMutableNotificationContent()
            {
                Title = "SwinApp",
                Body = text,
                Badge = 1
            };

            UNNotificationTrigger trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5D, false);

            string requestID = "SwinApp-notification";

            UNNotificationRequest request = UNNotificationRequest.FromIdentifier(requestID, content, trigger);

            UNUserNotificationCenter.Current.AddNotificationRequest(request, null);
        }
    }
}
