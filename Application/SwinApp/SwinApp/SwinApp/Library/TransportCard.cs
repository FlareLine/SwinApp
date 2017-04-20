using SwinApp.Components.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwinApp.Library
{
    class TransportCard : IDashCard
    {

    private Grid _content;

		public TransportCard()
		{
			_content = new CardTransport();
		}

		public string Title => "Next Trains";

		public Grid Content => _content;

		public void Load()
		{
		}

		public void Open()
		{
		}
    }
}
