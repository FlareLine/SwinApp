﻿using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SwinApp.Components.UI;

namespace SwinApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactsPage : ContentPage
	{
        public List<Grid> Contacts = new List<Grid>();

		public ContactsPage ()
        {
            Contacts.Add(new CardContact("Swinburne Main Contact", "", "", "+61 3 9214 8000", "Hawthorn Campus"));
            Contacts.Add(new CardContact("Swinburne Online", "", "", "1800 069 765", "Online"));
			Contacts.Add(new CardContact("Security", "Emergencies, Safety and Lost Property", "safercommunity@swin.edu.au", "+61 3 9214 3333", "Hawthorn Campus"));
            Contacts.Add(new CardContact("Student HQ", "Course advice, enrolment enquiries, fee information, ID cards", "", "1300 794 628", "Hawthorn Campus"));
			Contacts.Add(new CardContact("Swinburne Student Life", "Clubs and Events", "studentlife@swin.edu.au", "+61 3 9214 5445", "Hawthorn Campus"));
            Contacts.Add(new CardContact("Swinburne Student Union", "Independant, student-run representation, welfare and events service", "ssu@ssu.org.au", "+61 3 9214 5440", "Hawthorn Campus"));
            InitializeComponent ();
            ListContacts.ItemsSource = Contacts;
        }
	}
}
