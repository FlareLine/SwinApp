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

        public NewReminderPage ()
		{
			InitializeComponent ();
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
            _title = titleField.Text;
            _description = descriptionField.Text;
            _color = nameToColor[pickerColor.Items[pickerColor.SelectedIndex]];

            User.WriteReminder(new Reminder(_datetime, _title, _description, _color));
            await DisplayAlert("Success!", "Reminder was added :)", "close");
            await Application.Current.MainPage.Navigation.PopAsync();
            

        }
	}
}
