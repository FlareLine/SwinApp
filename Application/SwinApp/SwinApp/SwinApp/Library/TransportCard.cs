using SwinApp.Components.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SwinApp.Library
{
    class TransportCard : Grid, IDashCard, INotifyPropertyChanged
    {

		private TransportViewModel _tvm;
		private TrainDetailsGrid _content;
		public string Title { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public TransportCard(String title)
		{
			_tvm = new TransportViewModel();
			Title = title;
		}

		public TrainGrid Content
		{
			set
			{
				if (_content != value)
				{
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("DateTime"));
					}
				}
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
