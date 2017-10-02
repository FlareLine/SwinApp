using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SwinApp.Library;

namespace SwinApp.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardReminder : Grid
	{
        public Reminder _reminder;
		public CardReminder (Reminder reminder)
		{
            _reminder = reminder;
            BindingContext = _reminder;
			InitializeComponent ();

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) => {
                DeleteReminder();
            };
            ButtonDeleteReminder.GestureRecognizers.Add(tap);
        }

        public async void DeleteReminder()
        {

            var confirmDeletion = await App.Current.MainPage.DisplayAlert("Delete?", "Delete this reminder?", "Yes!", "No!");
            if (confirmDeletion)
            {
                var secondconfirmDeletion = await App.Current.MainPage.DisplayAlert("Are you sure?", "Do you 110% want to delete this reminder?", "Yes!", "No!");
                if (secondconfirmDeletion)
                {
                    User.DeleteReminder(_reminder);
                }
            }
            
        }

        private void ClickDelete(object sender, EventArgs e) => DeleteReminder();
    }
}
