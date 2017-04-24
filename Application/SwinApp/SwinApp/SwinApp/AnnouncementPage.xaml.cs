using SwinApp.Library;
using SwinApp.Components;
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
        private bool _isFiltering = false;
        private DateTime _filterDate = DateTime.Now.AddYears(1); // Initialize fresh date
        private ObservableCollection<BlackboardAnnouncement> _announcementFiltered = new ObservableCollection<BlackboardAnnouncement>();
        public AnnouncementPage()
        {
            InitializeComponent();
            ToolbarItems.Add(new ToolbarItem()
            {
                Text = "Refresh",
                Command = new Command(() => RefreshAnnouncements()),
                // TODO: ADD ICON
            });
        }

        private async void OpenAnnouncement(object sender, ItemTappedEventArgs e) => await Navigation.PushAsync(new DialogBBAnnouncement(ListAnnouncements.SelectedItem as BlackboardAnnouncement));

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshAnnouncements();
            ListAnnouncements.ItemsSource = _announcementFiltered;
            ListAnnouncements.ItemTapped += OpenAnnouncement;
            SwitchDate.Toggled += EnableFilter;
            DateAnnouncementFilter.DateSelected += ChangeFilter;
        }

        private void EnableFilter(object sender, ToggledEventArgs e) => ApplySwitchFilter();
        private void ChangeFilter(object sender, EventArgs e) => ApplySwitchFilter();
        /// <summary>
        /// Apply filter for dates if selected
        /// </summary>
        private void ApplySwitchFilter()
        {
            _isFiltering = SwitchDate.IsToggled;
            if (_isFiltering)
            {
                if (_filterDate.Date != DateAnnouncementFilter.Date.Date)
                {
                    _filterDate = DateAnnouncementFilter.Date;
                    _announcementFiltered = new ObservableCollection<BlackboardAnnouncement>(User.Announcements.Where(a => a.Created.Date == DateAnnouncementFilter.Date));
                }
            }
            else
                _announcementFiltered = new ObservableCollection<BlackboardAnnouncement>(User.Announcements);
            ListAnnouncements.ItemsSource = _announcementFiltered;
        }
        /// <summary>
        /// Refresh the available announcements to view
        /// </summary>
        /// <remarks>
        /// May be able to merge with ApplySwitchFilter
        /// </remarks>
        private void RefreshAnnouncements()
        {
            _announcementFiltered.Clear();
            ApplySwitchFilter();
        }
    }
}
