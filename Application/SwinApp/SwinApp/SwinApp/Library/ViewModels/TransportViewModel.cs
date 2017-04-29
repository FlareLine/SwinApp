using System;
using System.Threading.Tasks;

namespace SwinApp.Library
{
	public class TransportViewModel : ViewModel
	{
		public string Request { get; set; }

		public string Line { get; set; }
		public string Platform { get; set; }
		public string Time { get; set; }
		
		public PTVPayload Payload { get; set; }

		public TransportViewModel(string request)
		{
			Request = request;
		}

		public async Task Load()
		{
			Payload = await PTV.RequestPTVPayloadAsync(Request);
			Line = 
		}
	}
}