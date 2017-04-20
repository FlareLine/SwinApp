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
    public partial class GoogleMapPage : ContentPage
    {
        public GoogleMapPage()
        {
            InitializeComponent();
            WebMap.Source = @"https://www.google.com";
        }
    }
}
