using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp.Components.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardContact : Grid
	{
        private string _title;
        private string _description;
        private string _email;
        //private Uri _emailLink;
        private string _phone;
        //private string _phoneLink;
        private string _campus;


		public CardContact (string title, string description, string email, string phone, string campus)
		{
            _title = title;
            _description = description;
            _email = email;
            //_emailLink = new Uri("mailto:" + email);
            _phone = phone;
            //_phoneLink = "tel:" + phone;
            _campus = campus;
            BindingContext = this;
            InitializeComponent();
        }


        public string Title => _title;

        public string Description => _description;

        public string Email => _email;

        public string Phone => _phone;

        public string Campus => _campus;
    }
}
