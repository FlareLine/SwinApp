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

        Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua }, { "Black", Color.Black },
            { "Blue", Color.Blue }, { "Pink", Color.Pink },
            { "Gray", Color.Gray }, { "Green", Color.Green },
            { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
            { "Navy", Color.Navy }, { "Olive", Color.Olive },
            { "Purple", Color.Purple }, { "Red", Color.Red },
            { "Silver", Color.Silver }, { "Teal", Color.Teal },
            { "White", Color.White }, { "Yellow", Color.Yellow }
        };

        public NewAllocationPage()
        {
            InitializeComponent();

            for (int i = 1; i <= 52; i++)
                pickerWeeks.Items.Add(i.ToString());

            pickerWeeks.SelectedIndex = 0;

            dateField.MinimumDate = DateTime.Today;
            ButtonSubmit.Clicked += ClickSubmit;

            foreach (string colorName in nameToColor.Keys)
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
            _name = nameField.Text;
            _room = roomField.Text;
            _type = typeField.Text;
            _occurences = Int32.Parse(pickerWeeks.Items[pickerWeeks.SelectedIndex]);
            _color = nameToColor[pickerColor.Items[pickerColor.SelectedIndex]];

            List<TimetabledClass> tempClasses = new List<TimetabledClass>();

            for (int i = 0; i < _occurences; i++)
            {
                tempClasses.Add(new TimetabledClass(_datetime, _name, _room, _occurences, _type, _color));
                _datetime = _datetime.AddDays(7);
            }

            User.WriteTimetabledClasses(tempClasses);
            await DisplayAlert("Success!", "Class was added :)", "close");
            await Application.Current.MainPage.Navigation.PopAsync();
        }


    }
}