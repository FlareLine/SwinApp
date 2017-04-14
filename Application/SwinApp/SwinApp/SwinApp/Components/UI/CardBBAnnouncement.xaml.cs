using System;
using System.Collections.Generic;
using System.Text;
using SwinApp.Library;
using Xamarin.Forms;

namespace SwinApp.Components
{
    public partial class CardBBAnnouncement : Grid
    {
        private BlackboardAnnouncement _announcment;
        public CardBBAnnouncement(BlackboardAnnouncement announcement)
        {
            InitializeComponent();
            BindingContext = _announcment = announcement;
        }
    }
}
