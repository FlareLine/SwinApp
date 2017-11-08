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
            UpdateContext(planned);
            InitializeComponent ();
		}

        private void ModifyData(object sender, FocusEventArgs e)
        {
            _planned.Refresh();
        }

        public void UpdateContext(IPlanned planned)
        {
            _planned = planned;
            BindingContext = _planned;
        }
    }
}
