using System;
namespace SwinApp.Library.ViewModels
{
    public class ReminderViewModel
    {
        Reminder _reminder;

        public string TimeOfDay => _reminder.TimeOfDay;

        public string Date => _reminder.DateMonth;

        public string Day => _reminder.Day;

        public string Name => _reminder.Name;

        public string Description => _reminder.Description;

        public string HexColor => _reminder.HexColor;

        public ReminderViewModel(Reminder r)
        {
            _reminder = r;
        }

        public void DeleteReminder()
        {
            User.DeleteReminder(_reminder);
        }
    }
}
