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
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AnnouncementPage : ContentPage
	{
        private ObservableCollection<BlackboardAnnouncement> _announcementFiltered = new ObservableCollection<BlackboardAnnouncement>();
		public AnnouncementPage ()
		{
			InitializeComponent ();
            User.Announcements.ForEach(u => _announcementFiltered.Add(u));
            ListAnnouncementTime.ItemsSource = User.Announcements;
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
