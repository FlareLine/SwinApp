using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinApp.Library;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewReminderPage : ContentPage
	{
        private DateTime _datetime;
        private string _title;
        private string _description;
        
        

		public NewReminderPage ()
		{
			InitializeComponent ();
            dateField.MinimumDate = DateTime.Today;

        }


        void Submit()
        {
            _datetime = dateField.Date;
            _title = titleField.Text;
            _description = descriptionField.Text;

            User.WriteReminder(new Reminder(_datetime, _title, _description));
            DisplayAlert("Success!", "Reminder was added :)", "close");
            Application.Current.MainPage.Navigation.PopAsync();
        }
	}
}
