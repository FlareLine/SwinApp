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
    public partial class CardUpNext : Grid
	{
        private IPlanned _planned;
		public CardUpNext (IPlanned planned)
		{
            _planned = planned;
            BindingContext = _planned;
			InitializeComponent ();
		}

        private void ModifyData(object sender, FocusEventArgs e)
        {
            _planned.Refresh();
        }
    }
}
