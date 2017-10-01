using SwinApp.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp.Components.UI
{
    
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardScheduledTimetabledClass : Grid
	{

        public TimetabledClass _class;

		public CardScheduledTimetabledClass (TimetabledClass c)
		{
            _class = c;
            BindingContext = _class;
			InitializeComponent ();
		}
	}
}