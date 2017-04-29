﻿using SwinApp.Components;
using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransportPage : ContentPage
	{
        public TransportPage()
        {
            InitializeComponent();

			Train_Alamein.BindingContext = new TrainDetails("Alamein", "blah time", 2);
		}
	}
}
