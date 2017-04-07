using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SwinApp.Library;

namespace SwinApp
{
	public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			InitializeComponent();
            ListDashboard.ItemsSource = User.DashBoardItems;
            ListMenu.ItemsSource = new List<MenuItem>
            {
                new MenuItem("Timetable", "See your classes"),
                new MenuItem("Campus", "Find your way around"),
                new MenuItem("Transport", "Get home easily")
            };
		}
        protected override void OnAppearing()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            base.OnAppearing();
        }
    }
    public class MenuItem
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public MenuItem(string t, string d)
        {
            Title = t;
            Desc = d;
        }
    }
}
