using SwinApp.Components.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwinApp.Library
{
    class TransportCard : IDashCard
    {

		private TransportViewModel _tvm;
		private Grid _content;

		public TransportCard()
		{
			_tvm = new TransportViewModel();
			_content = new CardTransport(_tvm);
		}

		public string Title => "Next Trains";

		public Grid Content => _content;

		public void Load()
		{
			await _tvm.Load();
		}

		public void Open()
		{
		}
    }
}
