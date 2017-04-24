using System;
using System.Threading.Tasks;

namespace SwinApp.Library
{
	public class TransportViewModel : ViewModel
	{

		public string Line { get; set; }
		public string Request { get; set; }
		public PTVPayload Payload { get; set; }

		public async Task Load()
		{
			Payload = await PTV.RequestPTVPayloadAsync(Request);
		}
	}
}