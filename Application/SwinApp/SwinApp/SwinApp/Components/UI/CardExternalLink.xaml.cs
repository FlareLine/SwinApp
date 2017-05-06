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
	public partial class CardExternalLink : Grid
	{
        private Uri _URL;
        private string _title;
        private string _descripion;

		public CardExternalLink (string title, string description, Uri URL)
		{
            _URL = URL;
            _title = title;
            _descripion = description;
            BindingContext = this;
            InitializeComponent ();

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) => {
                OpenLink();
            };
            this.GestureRecognizers.Add(tap);
        }

        private void OpenLink()
        {
            Device.OpenUri(_URL);
        }

        public string Title => _title;

        public string Description => _descripion;

        private void ClickOpen(object sender, EventArgs e) => OpenLink();
    }
}
