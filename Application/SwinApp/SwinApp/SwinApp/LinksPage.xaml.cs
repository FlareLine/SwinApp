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
	public partial class LinksPage : ContentPage
	{
        public List<Grid> Links = new List<Grid>();

		public LinksPage ()
		{
            Links.Add(new CardExternalLink("Swinburne", "The site of our favourite university <3", new Uri("https://www.swinburne.edu.au/"), "swinburnelogo.png"));
            Links.Add(new CardExternalLink("Blackboard", "For all your unit needs", new Uri("http://ilearn.swin.edu.au/"), "blackboardlogo.png"));
            Links.Add(new CardExternalLink("Allocate", "For when that regular timetable just isn't cutting it", new Uri("https://allocate.swin.edu.au/aplus-2017/timetable/#subjects"), "allocatelogo.png"));
            Links.Add(new CardExternalLink("Study Spaces", "Find a spot where there's actually a powerpoint for your laptop", new Uri("http://m.swinburne.edu.au/study-spaces/#/"), "studyspaceslogo.png"));
            Links.Add(new CardExternalLink("News", "Keep up to date with the latest and greatest", new Uri("http://www.swinburne.edu.au/news/"), "newslogo.png"));
            Links.Add(new CardExternalLink("Staff", "Get the email address of your favourite tutor", new Uri("https://www.swin.edu.au/directory/"), "contactslogo.png"));
            Links.Add(new CardExternalLink("Library", "Get the books you need for that last minute assingment cram", new Uri("http://www.swinburne.edu.au/library/"), "librarylogo.png"));
            Links.Add(new CardExternalLink("Safe@Swin", "Everyone deserves a right to be comfortable at university", new Uri("https://play.google.com/store/apps/details?id=com.cutcom.apparmor.swin&hl=en"), "safeswinlogo.png"));
            Links.Add(new CardExternalLink("SSAA", "There to make your time at Swinburne as enjoyable as it can be", new Uri("https://www.swinburne.edu.au/current-students/life/student-organisations/"), "ssaalogo.png"));
            InitializeComponent ();
            //Some code that may or may not work
            ListLinks.ItemsSource = Links;
            ListLinks.ItemTapped += OpenLink;
        }

        private async void OpenLink(object sender, ItemTappedEventArgs e)
        {
            if (ListLinks.SelectedItem is CardExternalLink selectedPage)
            {
                var webPage = selectedPage.GetNewWebPage();
                Navigation.InsertPageBefore(webPage, this);

                var linksPage = await Navigation.PopAsync();
                //Navigation.InsertPageBefore(linksPage, webPage);  // Throws exceptions atm, may debug later
            }
        }
	}
}