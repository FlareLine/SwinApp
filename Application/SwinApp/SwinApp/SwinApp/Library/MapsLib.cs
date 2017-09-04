using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace SwinApp.Library
{
    public static class MapsLib
    {
        /// <summary>
        /// Dictionary containing most of the buildings in the Swinburne Hawthorn campus and their respective coordinates
        /// </summary>
        public static Dictionary<string, CoordinatePair> SwinBuildings_HAW = new Dictionary<string, CoordinatePair>()
        {
            { "LIB", new CoordinatePair("-37.822363", "145.0389899") },
            { "ATC", new CoordinatePair("-37.8226884", "145.036187") },
            { "AMDC", new CoordinatePair("-37.8229691", "145.0389097") },
            { "BA", new CoordinatePair("-37.8221247", "145.0391443") },
            { "AD", new CoordinatePair("-37.822128", "145.038771") },
            { "EN", new CoordinatePair("-37.8222208", "145.0374007") },
            { "EW", new CoordinatePair("-37.82198", "145.0372464") },
            { "GS", new CoordinatePair("-37.821345", "145.0379552") },
            { "SPS", new CoordinatePair("-37.8212475", "145.0376922") },
            { "SPW", new CoordinatePair("-37.8209918", "145.0369032") },
            { "AS", new CoordinatePair("-37.8225436", "145.0368979") },
            { "AV", new CoordinatePair("-37.8231452", "145.0413631") },
            { "IS", new CoordinatePair("-37.8229039", "145.04138") },
            { "AGSE", new CoordinatePair("-37.8213532", "145.0389196") },
            { "SR", new CoordinatePair("-37.8214917", "145.0382383") },
            { "TA", new CoordinatePair("-37.8209945", "145.0368032") },
            { "TB", new CoordinatePair("-37.8209596", "145.0373791") },
            { "TC", new CoordinatePair("-37.820569", "145.0374889") },
            { "TD", new CoordinatePair("-37.820312", "145.0370163") },
            { "SA", new CoordinatePair("-37.8222579", "145.0368806") },
            { "CH", new CoordinatePair("-37.8226523", "145.0356731") }
        };
    }

    public class CoordinatePair
    {
        public string X { get; set; }
        public string Y { get; set; }

        /// <summary>
        /// Creates a new coordinate pair with the specified coordinates
        /// </summary>
        /// <param name="x">x component of coordinate</param>
        /// <param name="y">y component of coordinate</param>
        public CoordinatePair(string x, string y)
        {
            X = x;
            Y = y;
        }
    }
}