using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SwinApp.Library;

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
            DependencyService.Get<INotification>().SetTimedNotification(_name + "(" + _type + ")" + " - " + _room, _datetime.AddMinutes(30) - DateTime.Now);
            await DisplayAlert("Success!", "Class was added :)", "close");
            await Application.Current.MainPage.Navigation.PopAsync();
        }


    }
}