using SwinApp.Components.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwinApp.Library
{
    class TransportCard : Grid, IDashCard
    {

		private TransportViewModel _tvm;
		private Grid _content;

		public TransportCard()
		{
			_tvm = new TransportViewModel();
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
