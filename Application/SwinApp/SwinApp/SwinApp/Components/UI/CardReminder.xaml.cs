using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SwinApp.Library;
using SwinApp.Library.ViewModels;

namespace SwinApp.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardReminder : Grid
	{
        ReminderViewModel _vm;
		public CardReminder (Reminder reminder)
		{
            _vm = new ReminderViewModel(reminder);
            BindingContext = _vm;
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
                    _vm.DeleteReminder();
                }
            }
            
        }

        private void ClickDelete(object sender, EventArgs e) => DeleteReminder();
    }
}
