using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SwinApp.Library;
using SwinApp.Components.UI;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentOffersPage : ContentPage
    {
        public List<Grid> Offers = new List<Grid>();

        public StudentOffersPage()
        {
            Offers.Add(new CardStudentOffer("Student Edge", "Student Edge is Australia’s largest member-based organisation of high school and tertiary students, with more than 900,000 members nationally.\nJoin now(for FREE) to access super sweet student discounts on Apple, McDonald’s, Microsoft, Chatime and more of your faves!", new Uri("https://studentedge.org/"), "studentedge.png"));
            InitializeComponent();
            ListOffers.ItemsSource = Offers;
        }
    }
}
