using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using SQLite;

namespace SwinApp.Library
{
    /// <summary>
    /// Used for linking Allocation data to a timetable easily
    /// </summary>
    public class AllocationViewModel : ViewModel
    {
        private Allocation _allocation;

        public string Description => _allocation.Subject.Description ?? "Invalid Allocation";

        public string Code => _allocation.Subject.Code ?? "Invalid Allocation";

        public string TimeOfDay => _allocation.Schedule.StartTime.ToString("hh:mm tt");

        public string When => $"{Day}, {TimeOfDay}";

        public string Room => _allocation.Schedule.Room.Code.Replace("HAW_", "");

        public string BuildingCode => System.Text.RegularExpressions.Regex.Replace(Room, "[0-9]", "");

        public string Type => _allocation.ActivityTypeReadable();

        public string TimeCardDescription => $"{Type}\n {Room}";

        public string Day => _allocation.DayOfWeek();

        public string DayShortened => Day.Substring(0, 3);

        public string Summary => $"{Type} in {Room}";

        public string MapSource => MapsLib.GetStaticMapURL(BuildingCode);

        public string MapClickUrl => $"https://www.google.com/maps/search/?api=1&query={MapsLib.SwinBuildings_HAW[BuildingCode].XY}";

        public Xamarin.Forms.Color Color
        {
            get
            {
                // Create table if not exists
                var conn = SwinDB.Conn;
                conn.CreateTable<AllocationColour>();
                // Assign the unit a colour code if not set already
                if (conn.Table<AllocationColour>().FirstOrDefault(a => a.SubjectCode == _allocation.Subject.Code) == null)
                {
                    int colorIndex = new Random().Next(0, 4);
                    string hexCode = $"TimeColor{colorIndex}";
                    conn.Insert(new AllocationColour()
                    {
                        SubjectCode = _allocation.Subject.Code,
                        HexCode = hexCode
                    });
                }

                return (Xamarin.Forms.Color)Application.Current.Resources[conn.Table<AllocationColour>()
                    .First(a => a.SubjectCode == _allocation.Subject.Code).HexCode
                    ];
            }
        }

        /// <summary>
        /// Mapping for DateTime values to the row in the timetable
        /// </summary>
        /// <remarks>
        /// In a perfect world, I'd change this entirely but what can ya do happens to the best of us
        /// </remarks>
        private readonly Dictionary<int, int> TimeRowValues = new Dictionary<int, int>()
        {
            [8] = 1,
            [9] = 3,
            [10] = 5,
            [11] = 7,
            [12] = 9,
            [13] = 11,
            [14] = 13,
            [15] = 15,
            [16] = 17,
            [17] = 19,
            [18] = 21,
            [19] = 23,
            [20] = 24
        };

        private int GridRow => TimeRowValues[_allocation.Schedule.StartTime.Hour] + (_allocation.Schedule.StartTime.Minute >= 30 ? 1 : 0);

        private int GridColumn => User.DayCompValues[Day] + 1;

        private int GridSpan => (int)Math.Floor((double)(_allocation.Schedule.Duration / 30));

        /// <summary>
        /// Generate an entry grid for the allocations
        /// </summary>
        /// <returns></returns>
        public Grid AllocationEntry()
        {
            Grid resGrid = new Grid
            {
                MinimumHeightRequest = 100,
                BackgroundColor = Color,
            };

            var tapped = new TapGestureRecognizer();
            tapped.Tapped += (send, ev) => Application.Current.MainPage.Navigation.PushAsync(new AllocationViewPage(this));
            resGrid.GestureRecognizers.Add(tapped);

            Grid.SetColumn(resGrid, GridColumn);
            Grid.SetRow(resGrid, GridRow);
            Grid.SetRowSpan(resGrid, GridSpan);

            resGrid.Children.Add(new Label()
            {
                Text = TimeCardDescription,
                FontSize = 14,
                Margin = new Thickness(2)
            });
            
            return resGrid;
        }

        public AllocationViewModel(Allocation allocation)
        {
            _allocation = allocation;
        }
    }
}
