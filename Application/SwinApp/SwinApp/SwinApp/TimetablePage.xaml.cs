using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SwinApp.Library;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimetablePage : ContentPage
	{
		private bool _populated = false;
		private readonly Color CURRENT_DAY_COLOR = Color.FromHex("#999897").MultiplyAlpha(0.2D);

		public TimetablePage()
		{
			InitializeComponent();
            for (int row = 1; row < GridTimetable.RowDefinitions.Count(); row += 2) {
                GridTimetable.Children.Add(new BoxView() { BackgroundColor = CURRENT_DAY_COLOR.MultiplyAlpha(0.8D) }, 0, 6, row, row + 1);
            }
		}

		/// <summary>
		/// On the first appearance of the timetable page, load the Current's semester allocations into view
		/// </summary>
		protected async override void OnAppearing()
		{
			base.OnAppearing();

			SwinDevice.Orientation = Orientation.Landscape;

			// Must manually use populated as otherwise we may clear off the default data
			if (!_populated)
			{
				ApplyCurrentDayColor();
				var contents = await GetGridContentsAsync();
				foreach (var c in contents)
					GridTimetable.Children.Add(c);
				_populated = true;
			}
		}

		/// <summary>
		/// Assign a darker background to the current day column
		/// </summary>
		private void ApplyCurrentDayColor()
		{
			BoxView currentDay = null;
			switch (DateTime.Today.DayOfWeek)
			{
				case DayOfWeek.Friday:
					currentDay = BoxViewFriday;
					break;
				case DayOfWeek.Monday:
					currentDay = BoxViewMonday;
					break;
				case DayOfWeek.Thursday:
					currentDay = BoxViewThursday;
					break;
				case DayOfWeek.Tuesday:
					currentDay = BoxViewTuesday;
					break;
				case DayOfWeek.Wednesday:
					currentDay = BoxViewWednesday;
					break;
				default:
					break;
			}
			if (currentDay != null)
				currentDay.Color = CURRENT_DAY_COLOR;
		}

		/// <summary>
		/// Asynchronously retrieve a lost of Allocation entries
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<Grid>> GetGridContentsAsync() => await Task.Run(() => User.CurrentSemesterAllocations.Select(a => new AllocationViewModel(a).AllocationEntry()));
	}
}
