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
	public partial class NewAllocationPage : ContentPage
	{
		public NewAllocationPage ()
		{
			InitializeComponent ();

            stepperWeeks.ValueChanged += onWeeksChange;

		}

        private void onWeeksChange(object sender, ValueChangedEventArgs e)
        {
            textWeeks.Text = String.Format("Weeks including first class: {0}", e.NewValue);
        }
	}
}