﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Xamarin.Forms;

namespace SwinApp.Library
{
    /// <summary>
    /// A Student class is the scope of SwinApp 
    /// </summary>
    public static class User
    {
        /// <summary>
        /// Used to allow for prototype dummy data
        /// </summary>
        public const bool USE_PROTOTYPE_DATA = true;

        private static ObservableCollection<IDashCard> _dashBoardItems = new ObservableCollection<IDashCard>();

        public static ObservableCollection<IDashCard> DashBoardItems
        {
            get { return _dashBoardItems; }
        }

        private static ObservableCollection<IDashCard> _scheduleItems = new ObservableCollection<IDashCard>();

        public static ObservableCollection<IDashCard> ScheduleItems => _scheduleItems;

        private static List<BlackboardAnnouncement> _announcements = new List<BlackboardAnnouncement>();

        public static List<BlackboardAnnouncement> Announcements => _announcements;

        private static List<BlackboardUnit> _units = new List<BlackboardUnit>();

        public static List<BlackboardUnit> Units => _units;

        private static List<Reminder> _reminders = new List<Reminder>();

        private static List<Lesson> _lessons = new List<Lesson>();

        public static List<Lesson> Lessons => _lessons;

        public static List<Reminder> Reminders => _reminders;

        public static Dictionary<string, string> UnitPairs => _units.ToDictionary(u => u.Name, u => u.UUID);

        public static async Task AddDashCard(IDashCard card)
        {
            await Task.Run(() => _dashBoardItems.Add(card));
        }
        private static void LoadBlackboardAnnouncements()
        {
            _announcements = new List<BlackboardAnnouncement>();
            if (USE_PROTOTYPE_DATA)
            {
                _announcements.Add(new BlackboardAnnouncement()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Test Blackboard Announcement",
                    Body = "Welcome to Blackboard, it's pretty sweet aye? Lots of cool stuff to mess with. \n Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem  \n Lorem Lorem Lorem Lorem Lorem Lorem Lorem Lorem \n Lorem ",
                    Created = DateTime.Now
                });
            }
        }
        private static void LoadBlackboardUnits()
        {
            _units = new List<BlackboardUnit>();
            if (USE_PROTOTYPE_DATA)
            {
                _units.Add(new BlackboardUnit()
                {
                    Name = "Test Unit",
                    Id = new Random().Next(100).ToString(),
                    UUID = Guid.NewGuid().ToString(),
                });
            }
        }
        public static void LoadUserData()
        {
            ClearDashItemsSafe();
            AddDashItemSafe(new TextContentDashCard("Welcome to SwinApp", "Creators of SwinApp"));
            LoadBlackboardAnnouncements();
            LoadBlackboardUnits();
            LoadLessons();
            foreach (BlackboardAnnouncement a in Announcements)
                AddDashItemSafe(new BBAnnouncementCard(a));
            if (USE_PROTOTYPE_DATA)
            {
                AddDashItemSafe(new TextContentDashCard("Remember, learning is fun", "Creators of SwinApp"));
                AddDashItemSafe(new UpNextCard(NextPlanned));
                AddDashItemSafe(new WeatherCard());
            }
            //if the file doesn't exist, set _reminders to be an empty List of Reminder
            _reminders = SwinIO<List<Reminder>>.Read("reminders.json") ?? new List<Reminder>();

            RefreshReminders();
        }
        private static IPlanned NextPlanned
        {
            get
            {
                List<IPlanned> _events = new List<IPlanned>();
                foreach (var l in _lessons)
                    _events.Add(l);
                foreach (var r in _reminders)
                    _events.Add(r);
                _events.Sort((r1, r2) => DateTime.Compare(r1.Time, r2.Time));
                return _events[0];
            }
        }
        /// <summary>
        /// Load lesson data
        /// </summary>
        public static void LoadLessons()
        {
            if (USE_PROTOTYPE_DATA)
            {
                _lessons.Add(new Lesson("Epic Lecture", DateTime.Today.AddHours(1), "EP1010", "EN1001", "Lecture"));
            }
        }

        public static async void WriteReminder(Reminder reminder)
        {
            _reminders.Add(reminder);
            await SwinIO<List<Reminder>>.WriteAsync("reminders.json", _reminders);
            RefreshReminders();

            //test code to see if remindrs are being stored, leave here for now in case it is needed later
            //_reminders.Clear();

            //_reminders = SwinIO<List<Reminder>>.Read("reminders.json");

            //string test = "";

            //foreach (Reminder r in _reminders){
            //    test += r.Name;
            //}

            //await Application.Current.MainPage.DisplayAlert("reminder output", test, "close");
        }

        public static async void DeleteReminder(Reminder reminder)
        {
            User.Reminders.RemoveAll(r => r == reminder);
            await SwinIO<List<Reminder>>.WriteAsync("reminders.json", User.Reminders);
        }

        /// <summary>
        /// Safely clear the dashitems of all its contents
        /// </summary>
        private static void ClearDashItemsSafe() => Device.BeginInvokeOnMainThread(() => _dashBoardItems.Clear());
        /// <summary>
        /// Safely add DashItem when using asynchronous threads
        /// </summary>
        /// <param name="card"></param>
        public static void AddDashItemSafe(IDashCard card) => Device.BeginInvokeOnMainThread(() => _dashBoardItems.Add(card));

        public static void AddScheduleItemSafe(IDashCard card) => Device.BeginInvokeOnMainThread(() => _scheduleItems.Add(card));

        //Causes unhanded exception
        public static void RemoveDashItemSafe(IDashCard cardToDelete)
        {
            _dashBoardItems.Remove(cardToDelete);
        }

        public static void RemoveScheduleItem(Grid grid)
        {          
            int index = -1;

            foreach (IDashCard c in _scheduleItems)
            {
                if (c.Content == grid)
                    index = _scheduleItems.IndexOf(c);
            }

            if (index != -1)
                _scheduleItems.RemoveAt(index);
        }

        //clear reminders, re add from array
        public static void RefreshReminders()
        {
            _scheduleItems.Clear();

            //sort by date
            _reminders.Sort((r1, r2) => DateTime.Compare(r1.Time, r2.Time));


            foreach (Reminder r in _reminders)
            {
                AddScheduleItemSafe(new ScheduledReminderCard(r));
            }

            foreach (Lesson l in _lessons)
            {
                AddScheduleItemSafe(new LessonCard(l));
            }
        }

        static User()
        {
        }
    }
}
