using SwinApp.Library;
using SwinApp.Library.ViewModels;
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

        public ClassViewModel _vm;

        //public TimetabledClass _class;

        public CardScheduledTimetabledClass(TimetabledClass c)
        {
            //_class = c;
            //BindingContext = _class;
            _vm = new ClassViewModel(c);
            BindingContext = _vm;
            InitializeComponent();


            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) =>
            {
                DeleteClass();
            };
            ButtonDeleteClass.GestureRecognizers.Add(tap);
        }

        public async void DeleteClass()
        {

            var confirmDeletion = await App.Current.MainPage.DisplayAlert("Delete?", "Delete this class?", "Yes!", "No!");
            if (confirmDeletion)
            {
                var secondconfirmDeletion = await App.Current.MainPage.DisplayAlert("Are you sure?", "Do you 110% want to delete this class?", "Yes!", "No!");
                if (secondconfirmDeletion)
                {
                    var MultipleDeletion = await App.Current.MainPage.DisplayAlert("Delete all?", "Delete all instances of this class?", "Yes!", "No, just delete this one!");
                    if (MultipleDeletion)
                    {
                        //get all classes that have the same name, day and time of day
                        User.DeleteTimetabledClasses(User.Classes.Where(r => r.Name == _vm.Name && r.Day == _vm.Day && r.TimeOfDay == _vm.TimeOfDay).ToList());
                    }
                    else
                    {
                        _vm.DeleteReminder();
                    }
                }

            }

        }

        private void ClickDelete(object sender, EventArgs e) => DeleteClass();
    }
}