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

        private static Dictionary<string, int> DayCompValues = new Dictionary<string, int>()
        {
            ["Monday"] = 0,
            ["Tuesday"] = 1,
            ["Wednesday"] = 2,
            ["Thursday"] = 3,
            ["Friday"] = 4
        };
        private static ObservableCollection<IDashCard> _dashBoardItems = new ObservableCollection<IDashCard>();

        public static ObservableCollection<IDashCard> DashBoardItems
        {
            get { return _dashBoardItems; }
        }

        private static ObservableCollection<IDashCard> _scheduleItems = new ObservableCollection<IDashCard>();

        public static ObservableCollection<IDashCard> ScheduleItems => _scheduleItems;


        private static List<Reminder> _reminders = new List<Reminder>();

        private static List<TimetabledClass> _classes = new List<TimetabledClass>();

        //Lessons need to be removed as they are deprecated, however keep for now as they are a part of NextPlanned (see comment above NextPlanned)

        public static List<Reminder> Reminders => _reminders;

        public static List<TimetabledClass> Classes => _classes;

        private static List<Allocation> _allocations = new List<Allocation>();

        public static List<Allocation> Allocations => _allocations;

        /// <summary>
        /// Get the allocations for the current semester ordered by day and time
        /// </summary>
        public static List<Allocation> CurrentSemesterAllocations => _allocations
            .Where(a => a.Schedule.StartDate < DateTime.Today && a.Schedule.EndDate > DateTime.Today)
            .OrderBy(a => DayCompValues[a.DayOfWeek()])
            .ThenBy(a => a.Schedule.StartTime)
            .ToList();

        public static ObservableCollection<IDashCard> ReminderCards = new ObservableCollection<IDashCard>();

        public static ObservableCollection<IDashCard> ClassesCards = new ObservableCollection<IDashCard>();

        private static UpNextCard _upNextCard;

        public static async Task AddDashCard(IDashCard card)
        {
            await Task.Run(() => _dashBoardItems.Add(card));
        }
        
        /// <summary>
        /// Loads all data relating to the User including blackboard data, timetables, trains, reminders etc...
        /// </summary>
        public static void LoadUserData()
        {
            ClearDashItemsSafe();
            AddDashItemSafe(new TextContentDashCard("Welcome to SwinApp", "Creators of SwinApp"));
            LoadUserTimetable();
            if (USE_PROTOTYPE_DATA)
            {
                AddDashItemSafe(new TextContentDashCard("Remember, learning is fun", "Creators of SwinApp"));
                AddDashItemSafe(_upNextCard);
                AddDashItemSafe(new WeatherCard());
            }
            //if the file doesn't exist, set _reminders to be an empty List of Reminder
            _reminders = SwinIO<List<Reminder>>.Read("reminders.json") ?? new List<Reminder>();
            _classes = SwinIO<List<TimetabledClass>>.Read("classes.json") ?? new List<TimetabledClass>();
        }

        //is broken, as _lessons are no longer used. Need to find a way to either convert reminders to allocations, or alternatively allow allocations to act as iPlanned
        private static IPlanned NextPlanned
        {
            get
            {
                List<IPlanned> _events = new List<IPlanned>();
               //need to put in allocations here
                foreach (var r in _reminders)
                    _events.Add(r);
                _events.Sort((r1, r2) => DateTime.Compare(r1.Time, r2.Time));
                return _events[0];
            }
        }
        /// <summary>
        /// Load lesson data
        /// </summary>


        public static async void WriteReminder(Reminder reminder)
        {
            _reminders.Add(reminder);
            _reminders.Sort((r1, r2) => DateTime.Compare(r1.Time, r2.Time));
            await SwinIO<List<Reminder>>.WriteAsync("reminders.json", _reminders);         
            PopulateSchedule();
        }

        public static async void DeleteReminder(Reminder reminder)
        {
            _reminders.RemoveAll(r => r == reminder);
            await SwinIO<List<Reminder>>.WriteAsync("reminders.json", User.Reminders);
            PopulateSchedule();
        }

        public static async void WriteTimetabledClasses(List<TimetabledClass> cList)
        {
            foreach (TimetabledClass c in cList)
                _classes.Add(c);
            _classes.Sort((r1, r2) => DateTime.Compare(r1.Time, r2.Time));
            await SwinIO<List<TimetabledClass>>.WriteAsync("classes.json", _classes);
            PopulateSchedule();
        }

        public static async void DeleteTimetabledClasses(List<TimetabledClass> cList)
        {
           foreach (TimetabledClass c in cList)
            {
                _classes.Remove(c);
            }
            await SwinIO<List<TimetabledClass>>.WriteAsync("classes.json", _classes);
            PopulateSchedule();
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

        /// <summary>
        /// Causes unhanded exception
        /// </summary>
        /// <param name="cardToDelete"></param>
        public static void RemoveDashItemSafe(IDashCard cardToDelete)
        {
            _dashBoardItems.Remove(cardToDelete);
        }

        /// <summary>
        /// Clear reminders, re-add from array
        /// </summary>
       

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
                PopulateSchedule();
            }
        }
        
        /// <summary>
        /// Using timetable data, populate the ScheduleCards table with IDashCards
        /// </summary>
        public static void PopulateSchedule()
        {
            //bit of a butched solution to make sure that we don't get duplicates of classes whenever a new reminder is added, should be fixed later
            ReminderCards.Clear();
            ClassesCards.Clear();

            //foreach (AllocationCard card in CurrentSemesterAllocations
            //    .Select(a => new AllocationCard(a)))
            //{
            //    ScheduleCards.Add(card);
            //}



            foreach (Reminder r in Reminders)
                ReminderCards.Add(new ScheduledReminderCard(r));

            foreach (TimetabledClass c in Classes)
                ClassesCards.Add(new ScheduledTimetabledClassCard(c));
            
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
