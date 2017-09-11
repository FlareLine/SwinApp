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
        private bool _populated = false;

		public TimetablePage ()
		{
			InitializeComponent ();
		}

        /// <summary>
        /// On the first appearance of the timetable page, load the Current's semester allocations into view
        /// </summary>
        protected async override void OnAppearing()
        {
            SwinDevice.Orientation = Orientation.Landscape;

            // Must manually use populated as otherwise we may clear off the default data
            if (!_populated)
            {
                var contents = await GetGridContentsAsync();
                foreach (var c in contents)
                    GridTimetable.Children.Add(c);
                _populated = true;
            }

        }

        public async Task<IEnumerable<Grid>> GetGridContentsAsync() => await Task.Run(() => User.CurrentSemesterAllocations.Select(a => new AllocationViewModel(a).AllocationEntry()));
    }
}
