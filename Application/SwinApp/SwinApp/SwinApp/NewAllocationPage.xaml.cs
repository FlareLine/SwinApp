using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewAllocationPage : ContentPage
    {

        private DateTime _datetime;
        private string _name;
        private string _room;
        private int _occurences;
        private string _type;
        private Color _color;
        private DateTime _scheduledDateTime;

        private List<String> classTypes = new List<string>()
        {
            "Lecture",
            "Tutorial",
            "Lab",
            "Workshop",
            "Seminar",
            "Meeting",
            "Other"
        };

        public NewAllocationPage()
        {
            InitializeComponent();

            for (int i = 1; i <= 52; i++)
                pickerWeeks.Items.Add(i.ToString());

            pickerWeeks.SelectedIndex = 0;

            dateField.MinimumDate = DateTime.Today;
            ButtonSubmit.Clicked += ClickSubmit;

            foreach (string colorName in Clrs.timetableNameToColor.Keys)
            {
                pickerColor.Items.Add(colorName);
            }

            foreach (string type in classTypes)
            {
                pickerType.Items.Add(type);
            }

            pickerColor.SelectedIndex = 0;
            pickerType.SelectedIndex = 0;

        }

        private async void ClickSubmit(object sender, EventArgs e) => await Submit();


        public async Task Submit()
        {
            _datetime = dateField.Date;
            _datetime += timeField.Time;
            _name = nameField.Text;
            _room = roomField.Text;
            _type = pickerType.Items[pickerType.SelectedIndex];
            _occurences = Int32.Parse(pickerWeeks.Items[pickerWeeks.SelectedIndex]);
            _color = Clrs.timetableNameToColor[pickerColor.Items[pickerColor.SelectedIndex]];

            List<TimetabledClass> tempClasses = new List<TimetabledClass>();

            for (int i = 0; i < _occurences; i++)
            {
                tempClasses.Add(new TimetabledClass(_datetime, _name, _room, _occurences, _type, _color));
                _datetime = _datetime.AddDays(7);
            }

            User.WriteTimetabledClasses(tempClasses);
            await DisplayAlert("Success!", "Class was added :)", "close");
            await Application.Current.MainPage.Navigation.PopAsync();

            if (SettingsPage.autoClassNotify)
			{
                //add notification to occur 15 minutes before the class
                _scheduledDateTime = _datetime.AddMinutes(-15); //Subtract 15 minutes from the actual time of the class
				DependencyService.Get<INotification>().SetTimedNotification("SWINAPP", _datetime - DateTime.Now); //Schedule the notification
				await Application.Current.MainPage.Navigation.PopAsync();
                			}
        }


    }
}