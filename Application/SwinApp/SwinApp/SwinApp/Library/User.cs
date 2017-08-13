using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Threading;
using Xamarin.Forms;
using System.Net.Http;

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

        private static List<Allocation> _allocations = new List<Allocation>();

        public static List<Allocation> Allocations => _allocations;

        public static List<Allocation> CurrentSemesterAllocations => _allocations
                .Where(a => a.Schedule.StartDate < DateTime.Today && a.Schedule.EndDate > DateTime.Today).ToList();

        public static ObservableCollection<AllocationCard> TimetableCards;

        public static Dictionary<string, string> UnitPairs => _units.ToDictionary(u => u.Name, u => u.UUID);

        private static UpNextCard _upNextCard;

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
        /// <summary>
        /// Loads all data relating to the User including blackboard data, timetables, trains, reminders etc...
        /// </summary>
        public static void LoadUserData()
        {
            ClearDashItemsSafe();
            AddDashItemSafe(new TextContentDashCard("Welcome to SwinApp", "Creators of SwinApp"));
            LoadBlackboardAnnouncements();
            LoadBlackboardUnits();
            LoadLessons();
            LoadUserTimetable();
            foreach (BlackboardAnnouncement a in Announcements)
                AddDashItemSafe(new BBAnnouncementCard(a));
            if (USE_PROTOTYPE_DATA)
            {
                AddDashItemSafe(new TextContentDashCard("Remember, learning is fun", "Creators of SwinApp"));
                _upNextCard = new UpNextCard(NextPlanned);
                AddDashItemSafe(_upNextCard);
                AddDashItemSafe(new WeatherCard());
            }
            //if the file doesn't exist, set _reminders to be an empty List of Reminder
            _reminders = SwinIO<List<Reminder>>.Read("reminders.json") ?? new List<Reminder>();

            RefreshSchedule();
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
                _lessons = new List<Lesson>
                {
                    new Lesson("Epic Lecture", DateTime.Today.AddHours(1), "EP1010", "EN1001", "Lecture"),
                    new Lesson("Awesome Tute", DateTime.Today.AddHours(1.5), "AT1234", "ATC0420", "Tutorial", Color.FromHex("#E0B4E8")),
                    new Lesson("Powerful Tute", DateTime.Today.AddMinutes(40), "AT1235", "ATC0430", "Tutorial", Color.FromHex("#6E9685")),
                    new Lesson("Inspirational Lecture", DateTime.Today.AddHours(3.5), "AT1234", "ATC0420", "Tutorial", Color.FromHex("#818BFF"))
                };
            }
        }

        public static async void WriteReminder(Reminder reminder)
        {
            _reminders.Add(reminder);
            await SwinIO<List<Reminder>>.WriteAsync("reminders.json", _reminders);
            RefreshSchedule();

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

        /// <summary>
        /// Causes unhanded exception
        /// </summary>
        /// <param name="cardToDelete"></param>
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

        /// <summary>
        /// Clear reminders, re-add from array
        /// </summary>
        public static void RefreshSchedule()
        {
            _scheduleItems.Clear();

            
            _reminders.Sort((r1, r2) => DateTime.Compare(r1.Time, r2.Time));
            _lessons.Sort((l1, l2) => DateTime.Compare(l1.Time, l2.Time));

            foreach (Lesson l in _lessons)
            {
                AddScheduleItemSafe(new LessonCard(l));
            }

            foreach (Reminder r in _reminders)
            {
                AddScheduleItemSafe(new ScheduledReminderCard(r));
            }

        }

        /// <summary>
        /// Asynchronously load the user timetable data, assign it to the User's
        /// Allocation variable
        /// </summary>
        /// <returns></returns>
        public static async void LoadUserTimetable()
        {
            const bool USE_REAL_DATA = false;
            const string TEST_ENDPOINT = USE_REAL_DATA ? "https://api-sit-proxy.swin.edu.au/v2/timetable/student/101091995" : "https://gist.githubusercontent.com/pielegacy/a0e81e05118f8eefb38283419cee1539/raw/9d716456c6331e5d587acd16cc5ef44fdfe3f920/alex-timetable-payload.xml";
            using (HttpClient client = new HttpClient())
            {
                string res = await client.GetStringAsync(TEST_ENDPOINT);
                ProcessTimetableDump(res);
                PopulateTimetableCards();
            }
        }
        
        /// <summary>
        /// Using timetable data, populate the TimetableCards table with IDashCards
        /// </summary>
        private static void PopulateTimetableCards()
        {
            TimetableCards = new ObservableCollection<AllocationCard>(CurrentSemesterAllocations
                .Select(a => new AllocationCard(a)));
        }

        /// <summary>
        /// Using XMLDocument process the timetable payload into usable
        /// Allocation objects
        /// </summary>
        /// <param name="data">The XML string</param>
        /// <returns></returns>
        private static void ProcessTimetableDump(string data)
        {
            XDocument doc = XDocument.Parse(data);
            _allocations = new List<Allocation>();
            var allocations = doc.Root.Elements("allocation");
            foreach (var a in allocations)
            {
                Allocation temp = new Allocation();
                temp.Import(a.ToString());
                _allocations.Add(temp);
            }
        }

        static User()
        {
        }
    }
}
