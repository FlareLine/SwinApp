using System;
using System.Threading.Tasks;

namespace SwinApp.Library
{
	public class TrainCardViewModel : ViewModel
	{
		public string Line { get; set; }
		public string Time { get; set; }
		public string Platform { get; set; }

		public TrainCardViewModel()
		{
		}
	}
}