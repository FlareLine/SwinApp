using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoogleMapPage : ContentPage
    {
        public GoogleMapPage()
        {
            InitializeComponent();
            WebMap.Source = "https://www.google.com/maps/d/viewer?mid=1XWZ-gWAvWTQrkiVk_59U3GOu_8I";
        }
    }
}
