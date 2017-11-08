using System;

namespace SwinApp.Library
{
    public interface INotification
    {
        void ShowTextNotification(string text);

        void SetTimedNotification(string text, TimeSpan when);
    }
}
