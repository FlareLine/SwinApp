using SwinApp.Components.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SwinApp.Library
{
    public class TransportCard : Grid, IDashCard
    {

		private TransportViewModel _tvm;
		private TrainDetailsGrid _content;
		public string Title { get; set; }

		//public event PropertyChangedEventHandler PropertyChanged;

		public TransportCard(String title)
		{
			_tvm = new TransportViewModel();
			Title = title;
		}

		public Grid Content
		{
            // What in tarnation are you trying with this?
            // You only databind value
			set
			{
				//if (_content != value)
				//{
				//	if (PropertyChanged != null)
				//	{
				//		PropertyChanged(this, new PropertyChangedEventArgs("DateTime"));
				//	}
				//}
			}
			get
			{
				return _content;
			}
		}

		public void Load()
		{
		}

		public void Open()
		{
		}
    }
}
