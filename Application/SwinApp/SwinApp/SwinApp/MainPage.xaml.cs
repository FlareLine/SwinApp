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
        public Student _scope;
		public MainPage(Student scope)
		{
            _scope = scope;
			InitializeComponent();
            ListDashboard.ItemsSource = scope.DashBoardItems;
		}
        protected override void OnAppearing()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            base.OnAppearing();
        }
    }
}
