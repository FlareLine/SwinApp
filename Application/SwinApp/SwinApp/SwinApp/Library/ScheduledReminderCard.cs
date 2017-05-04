using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SwinApp.Components;

namespace SwinApp.Library
{
    class ScheduledReminderCard : IDashCard
    {
        private Grid _content;
        private Reminder _reminder;

        public ScheduledReminderCard(Reminder reminder)
        {
            _reminder = reminder;
            _content = new CardReminder(_reminder);
        }

        public string Title => "Reminder";

        public Grid Content => _content;

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }
}
