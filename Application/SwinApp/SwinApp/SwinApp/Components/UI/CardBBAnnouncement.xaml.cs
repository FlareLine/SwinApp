using System;
using System.Collections.Generic;
using System.Text;
using SwinApp.Library;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardBBAnnouncement : Grid
    {
        private BlackboardAnnouncement _announcment;
        public CardBBAnnouncement(BlackboardAnnouncement announcement)
        {
            InitializeComponent();
            BindingContext = _announcment = announcement;
            var expandGesture = new TapGestureRecognizer();
            expandGesture.Tapped += ExpandDetails;
            TextBody.GestureRecognizers.Add(expandGesture);
            var viewAllGesture = new TapGestureRecognizer();
            viewAllGesture.Tapped += ViewAll;
            GridShowAll.GestureRecognizers.Add(viewAllGesture);
        }

        private async void ViewAll(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AnnouncementPage());
        }

        private void ExpandDetails(object sender, EventArgs e)
        {
            TextBody.LineBreakMode = TextBody.LineBreakMode == LineBreakMode.TailTruncation ? LineBreakMode.WordWrap : LineBreakMode.TailTruncation;
        }
    }
}
