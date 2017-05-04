﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SwinApp.Library;

namespace SwinApp.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardReminder : Grid
	{
        public Reminder _reminder;
		public CardReminder (Reminder reminder)
		{
            _reminder = reminder;
            BindingContext = _reminder = reminder;
			InitializeComponent ();
		}
	}
}