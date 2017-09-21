using System;
using System.Collections.Generic;
using System.Text;

namespace SwinApp.Library
{
    public interface INotification
    {
        void ShowTextNotification(string text);

        void SetTimedNotification(string text, TimeSpan when);
    }
}
