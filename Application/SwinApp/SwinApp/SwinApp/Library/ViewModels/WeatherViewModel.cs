using System.Threading.Tasks;

namespace SwinApp.Library
{
    public class WeatherViewModel : ViewModel
    {
        public WeatherConnection Conn = new WeatherConnection();
        private string _desc = "Loading";
        public async Task Load()
        {
            await Conn.DownloadWeatherAsync();
            NotifyPropertyChanged("Description");
            NotifyPropertyChanged("Remarks");
        }
        public string Description => Conn.Weather.Description;

        public string Remarks
        {
            get
            {
                if (Conn.Weather.Current > 100)
                    return "You should be dead, not at uni.";
                else if (Conn.Weather.Current > 18 && Conn.Weather.Current < 28)
                    return "Not too hot, not too cold.";
                else if (Conn.Weather.Current > 28)
                    return "Quite a hot one today, stay hydrated.";
                else
                    return "Bit chilly today, might need a jumper.";
            }
        }
    }
}
