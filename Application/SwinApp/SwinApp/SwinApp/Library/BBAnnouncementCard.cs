using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SwinApp.Components;

namespace SwinApp.Library
{
    public class BBAnnouncementCard : IDashCard
    {
        public string Title => "Recent Announcement";

        public Grid Content => _card;

        private CardBBAnnouncement _card;

        public BBAnnouncementCard(BlackboardAnnouncement announcement)
        {
            _card = new CardBBAnnouncement(announcement);
            //_card.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => Open()) });
        }
        public void Load()
        {
            throw new NotImplementedException();
        }
        public void Open()
        {
            Application.Current.MainPage.DisplayAlert("Works", "works", "works");
        }
    }
}
