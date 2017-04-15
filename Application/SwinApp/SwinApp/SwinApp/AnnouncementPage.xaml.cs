using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
	public partial class AnnouncementPage : ContentPage
	{
        private ObservableCollection<BlackboardUnit> _unitFilters;
		public AnnouncementPage ()
		{
			InitializeComponent ();
            User.Units.ForEach(u => _unitFilters.Add(u));
            ListAnnouncementTime.ItemsSource = _unitFilters;
            PickerAnnouncementFilter.Items.Add("All");
            PickerAnnouncementFilter.SelectedIndex = 0;
            foreach (var u in User.UnitPairs)
                PickerAnnouncementFilter.Items.Add(u.Key);
            PickerAnnouncementFilter.SelectedIndexChanged += ChangeFilter;
        }

        private void ChangeFilter(object sender, EventArgs e)
        {

        }
    }
}
