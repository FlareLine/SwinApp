﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwinApp.Components.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardStudentOffer : Grid
    {
        private Uri _URL;
        private string _title;
        private string _descripion;
        private string _imageAddress;


        public CardStudentOffer(string title, string description, Uri URL, string address)
        {
            _URL = URL;
            _title = title;
            _descripion = description;
            _imageAddress = address;
            BindingContext = this;
            InitializeComponent();

            //TapGestureRecognizer tap = new TapGestureRecognizer();
            //tap.Tapped += async (object sender, EventArgs e) => {
            //    await OpenLink();
            //};
            //this.GestureRecognizers.Add(tap);

            OfferImage.Source = ImageSource.FromFile(_imageAddress);
        }

        //private async Task OpenLink()
        //{
        //    await Navigation.PushAsync(new WebsitePage(_URL, _title));
        //    //Device.OpenUri(_URL);
        //}

        public Page GetNewWebPage()
        {
            return new WebsitePage(_URL, _title);
        }

        public string Title => _title;

        public string Description => _descripion;

        public string ImageAddress => _imageAddress;

        //private void ClickOpen(object sender, EventArgs e) => OpenLink();
    }
}
