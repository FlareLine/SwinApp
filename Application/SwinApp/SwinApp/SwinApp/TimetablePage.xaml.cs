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
	public partial class TimetablePage : ContentPage
	{
		public TimetablePage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// On the first appearance of the timetable page, load the Current's semester allocations into view
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ListAllocations.ItemsSource == null)
                ListAllocations.ItemsSource = User.CurrentSemesterAllocations
                    .Select(a => new AllocationViewModel(a));
        }
    }
}
