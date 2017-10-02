using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinApp.Library;
using SwinApp.Library.Analytics;
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
        private Color _color;

        public NewReminderPage ()
		{
			InitializeComponent ();
            dateField.MinimumDate = DateTime.Today;
            ButtonSubmit.Clicked += ClickSubmit;

            foreach (string colorName in Clrs.timetableNameToColor.Keys)
            {
                pickerColor.Items.Add(colorName);
            }

            pickerColor.SelectedIndex = 0;
        }

        private async void ClickSubmit(object sender, EventArgs e) => await Submit();

        public async Task Submit()
        {
            
            _datetime = dateField.Date;
            _datetime += timeField.Time;
            _title = titleField.Text;
            _description = descriptionField.Text;
            _color = Clrs.timetableNameToColor[pickerColor.Items[pickerColor.SelectedIndex]];

            User.WriteReminder(new Reminder(_datetime, _title, _description, _color));
            await DisplayAlert("Success!", "Reminder was added :)", "close");
            await Analytics.LogEventAsync(new AppEvent(EventType.REMINDER_CREATE, DateTime.Now));
            await Application.Current.MainPage.Navigation.PopAsync();
            

        }
	}
}
