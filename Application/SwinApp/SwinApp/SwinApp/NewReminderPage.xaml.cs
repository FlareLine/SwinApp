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
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewReminderPage : ContentPage
	{
        private DateTime _datetime;
        private string _title;
        private string _description;
        
        

		public NewReminderPage ()
		{
			InitializeComponent ();
            dateField.MinimumDate = DateTime.Today;
            ButtonSubmit.Clicked += ClickSubmit;
        }

        private async void ClickSubmit(object sender, EventArgs e) => await Submit();

        public async Task Submit()
        {
            _datetime = dateField.Date;
            _datetime += timeField.Time;
            _title = titleField.Text;
            _description = descriptionField.Text;

            User.WriteReminder(new Reminder(_datetime, _title, _description));
            await DisplayAlert("Success!", "Reminder was added :)", "close");
            await Application.Current.MainPage.Navigation.PopAsync();
            

        }
	}
}
