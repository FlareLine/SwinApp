using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebsitePage : ContentPage
    {
        public WebsitePage(Uri URL, string title)
        {
            InitializeComponent();
            Website.Source = URL.AbsoluteUri;
            this.Title = title;
        }
    }
}