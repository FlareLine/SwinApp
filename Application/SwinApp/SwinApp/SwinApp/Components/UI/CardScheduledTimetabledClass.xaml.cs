using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp.Components.UI
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardScheduledTimetabledClass : Grid
    {

        public TimetabledClass _class;

        public CardScheduledTimetabledClass(TimetabledClass c)
        {
            _class = c;
            BindingContext = _class;
            InitializeComponent();


            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) =>
            {
                DeleteReminder();
            };
            ButtonDeleteClass.GestureRecognizers.Add(tap);
        }

        public async void DeleteReminder()
        {

            var confirmDeletion = await App.Current.MainPage.DisplayAlert("Delete?", "Delete this class?", "Yes!", "No!");
            if (confirmDeletion)
            {
                var secondconfirmDeletion = await App.Current.MainPage.DisplayAlert("Are you sure?", "Do you 110% want to delete this class?", "Yes!", "No!");
                if (secondconfirmDeletion)
                {
                    List<TimetabledClass> toDelete = new List<TimetabledClass>();
                    var MultipleDeletion = await App.Current.MainPage.DisplayAlert("Delete all?", "Delete all instances of this class?", "Yes!", "No, just delete this one!");
                    if (MultipleDeletion)
                    {
                        //get all classes that have the same name, day and time of day
                        toDelete = User.Classes.Where(r => r.Name == _class.Name && r.Day == _class.Day && r.TimeOfDay == _class.TimeOfDay).ToList();
                    }
                    else
                    {
                        toDelete.Add(_class);
                    }
                    User.DeleteTimetabledClasses(toDelete);
                }

            }

        }

        private void ClickDelete(object sender, EventArgs e) => DeleteReminder();
    }



}