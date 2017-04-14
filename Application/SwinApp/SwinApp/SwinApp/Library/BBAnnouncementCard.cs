using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SwinApp.Components;

namespace SwinApp.Library
{
    public class BBAnnouncementCard : IDashCard
    {
        public string Title => "Blackboard Announcement";

        public Grid Content => _card;

        private CardBBAnnouncement _card;

        public BBAnnouncementCard(BlackboardAnnouncement announcement)
        {
            _card = new CardBBAnnouncement(announcement);
        }
        public void Load()
        {
            throw new NotImplementedException();
        }
    }
}
