﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public string Room => _allocation.Schedule.Room.Code.Replace("HAW_", "");

        public string Type => _allocation.ActivityTypeReadable();

        public string Summary => $"{Type} in {Room}";

        public AllocationViewModel(Allocation allocation)
        {
            _allocation = allocation;
        }
    }
}