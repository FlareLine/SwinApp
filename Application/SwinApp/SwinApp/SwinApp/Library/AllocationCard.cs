using SwinApp.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwinApp.Library
{
    /// <summary>
    /// Wrapper class for the CardAllocation class, used
    /// to bind the ViewModel to the UI itself
    /// </summary>
    public class AllocationCard : IDashCard
    {
        private AllocationViewModel _vm;
        private Grid _content;

        public string Title => "Allocation";

        public Grid Content => _content;

        public AllocationCard(Allocation allocation)
        {
            _vm = new AllocationViewModel(allocation);
            _content = new CardAllocation(_vm);
        }

        public void Load()
        {
        }

        public void Open()
        {
        }
    }
}
