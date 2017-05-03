using System;
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
        private IPlanned _reminder;
		public CardReminder (IPlanned reminder)
		{
            _reminder = reminder;
			InitializeComponent ();
		}
	}
}
