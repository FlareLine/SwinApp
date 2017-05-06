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
            Links.Add(new CardExternalLink("Swinburne", "The site of our favourite university <3", new Uri("https://www.swinburne.edu.au/")));
            Links.Add(new CardExternalLink("Blackboard", "For all your unit needs", new Uri("http://ilearn.swin.edu.au/")));
            Links.Add(new CardExternalLink("Allocate", "For when that regular timetable just isn't cutting it", new Uri("https://allocate.swin.edu.au/aplus-2017/timetable/#subjects")));
            Links.Add(new CardExternalLink("Study Spaces", "Find a spot where there's actually a powerpoint for your laptop", new Uri("http://m.swinburne.edu.au/study-spaces/#/")));
            Links.Add(new CardExternalLink("News", "Keep up to date with the latest and greatest", new Uri("http://www.swinburne.edu.au/news/")));
            Links.Add(new CardExternalLink("Staff", "Get the email address of your favourite tutor", new Uri("https://www.swin.edu.au/directory/")));
            Links.Add(new CardExternalLink("Llibrary", "Get the books you need for that last minute assingment cram", new Uri("http://www.swinburne.edu.au/library/")));
            Links.Add(new CardExternalLink("Safe@Swin", "Everyone deserves a right to be comfortable at university", new Uri("https://play.google.com/store/apps/details?id=com.cutcom.apparmor.swin&hl=en")));
            Links.Add(new CardExternalLink("SSAA", "There to make your time at Swinburne as enjoyable as it can be", new Uri("https://www.swinburne.edu.au/current-students/life/student-organisations/")));
            InitializeComponent ();
            ListLinks.ItemsSource = Links;
        }
	}
}
