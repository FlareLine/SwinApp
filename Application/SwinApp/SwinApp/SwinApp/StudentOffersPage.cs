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
            Offers.Add(new CardStudentOffer("Student Edge", "Student Edge is Australia’s largest member-based organisation of high school and tertiary students, with more than 900,000 members nationally.\nJoin now(for FREE) to access super sweet student discounts on Apple, McDonald’s, Microsoft, Chatime and more of your faves!", new Uri("https://studentedge.org/"), "StudentEdgeLogo.png"));
            Offers.Add(new CardStudentOffer("Swinburne ELMS", "The Electronic License Management System (ELMS) portal provides an interface for eligible STEM students to obtain license codes and download media for select VMware and Microsoft development tools.", new Uri("https://elms.swin.edu.au/"), "ELMSLogo.png"));
                /*Text taken directly from Swinburne ELMS portal (on 03/09/17)
                 * Logo currently just a placeholder (03/09/17) */
            //Maybe add ISIC (isic.com.au)
            //Add UNiDAYS if they get back to me
            InitializeComponent();
            ListOffers.ItemsSource = Offers;
        }
    }
}
