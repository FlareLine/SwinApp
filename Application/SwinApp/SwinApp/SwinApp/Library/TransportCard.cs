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

		}

		public string Title => "Next Trains";

		public Grid Content => _content;

		public void Load()
		{
			throw new NotImplementedException();
		}

		public void Open()
		{
			throw new NotImplementedException();
		}
	}
}
