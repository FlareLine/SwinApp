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
        public Reminder _reminder;
		public CardReminder (Reminder reminder)
		{
            _reminder = reminder;
            BindingContext = _reminder;
			InitializeComponent ();
            ButtonDeleteReminder.Clicked += ClickDelete;
		}

        public void DeleteReminder()
        {
            User.DeleteReminder(_reminder);
        }

        private void ClickDelete(object sender, EventArgs e) => DeleteReminder();
    }
}
