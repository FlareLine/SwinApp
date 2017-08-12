﻿using System;
using System.Collections.Generic;
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

        public AllocationViewModel(Allocation allocation)
        {
            _allocation = allocation;
        }
    }
}
