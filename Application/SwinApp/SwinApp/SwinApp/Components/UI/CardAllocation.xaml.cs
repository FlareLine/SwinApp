using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardAllocation : Grid
	{
        private AllocationViewModel _vm;

		public CardAllocation (AllocationViewModel vm)
		{
			InitializeComponent ();
            _vm = vm;
            BindingContext = _vm;
		}

        /// <summary>
        /// Add a touch listener 
        /// </summary>
        private void AddFrameTapGestureRecognizer()
        {
            if (FrameAllocation.GestureRecognizers.Count == 0)
            {
                TapGestureRecognizer gest = new TapGestureRecognizer();
                gest.Tapped += async (send, ev) => await NavigateToViewPage();
                FrameAllocation.GestureRecognizers.Add(gest);
            }
        }

        /// <summary>
        /// Navigate to the associated details page for the Allocation
        /// </summary>
        private async Task NavigateToViewPage() => await Application.Current.MainPage.Navigation.PushAsync(new AllocationViewPage(_vm));

        /// <summary>
        /// Only apply gesture recognizers upon the parent being visible
        /// </summary>
        protected override void OnParentSet()
        {
            base.OnParentSet();
            AddFrameTapGestureRecognizer();
        }
    }
}
