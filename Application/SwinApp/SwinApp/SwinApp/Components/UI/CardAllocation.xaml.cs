﻿using SwinApp.Library;
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
            AddFrameTapGestureRecognizer();
		}

        private void AddFrameTapGestureRecognizer()
        {
            TapGestureRecognizer gest = new TapGestureRecognizer();
            gest.Tapped += async (send, ev) => await Navigation.PushAsync(new TimetablePage());
            FrameAllocation.GestureRecognizers.Add(gest);
        }
	}
}