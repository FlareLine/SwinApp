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
        private string _imageAddress;


		public CardExternalLink (string title, string description, Uri URL, string address)
		{
            _URL = URL;
            _title = title;
            _descripion = description;
            _imageAddress = address;
            BindingContext = this;
            InitializeComponent ();

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (object sender, EventArgs e) => {
                OpenLink();
            };
            this.GestureRecognizers.Add(tap);

            menuIcon.Source = ImageSource.FromFile(_imageAddress);
              

            
        }

        private void OpenLink()
        {
            Device.OpenUri(_URL);
        }

        public string Title => _title;

        public string Description => _descripion;

        public string ImageAddress => _imageAddress;

        private void ClickOpen(object sender, EventArgs e) => OpenLink();
    }
}
