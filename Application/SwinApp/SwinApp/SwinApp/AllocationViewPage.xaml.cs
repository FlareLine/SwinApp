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
	public partial class AllocationViewPage : ContentPage
	{
        private AllocationViewModel _vm;

		public AllocationViewPage (AllocationViewModel vm)
		{
			InitializeComponent ();
            BindingContext = _vm = vm;
		}
	}
}