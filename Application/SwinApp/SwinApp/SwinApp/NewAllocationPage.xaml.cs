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

            for (int i = 1; i <= 52; i++)
                pickerWeeks.Items.Add(i.ToString());

            pickerWeeks.SelectedIndex = 0;

		}

        
	}
}