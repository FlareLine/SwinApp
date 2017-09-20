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
            { "LIB", new CoordinatePair("-37.8224001" ,"145.0392368") },
            { "ATC", new CoordinatePair("-37.8227126", "45.0383664") },
            { "AMDC", new CoordinatePair("-37.8228562", "145.0392797") },
            { "BA", new CoordinatePair("-37.8221143", "145.0394156") },
            { "AD", new CoordinatePair("-37.8221052", "145.0387633") },
            { "EN", new CoordinatePair("-37.8222138", "145.0379439") },
            { "EW", new CoordinatePair("-37.8220051", "145.037439") },
            { "GS", new CoordinatePair("-37.8212887", "145.0385098") },
            { "SPS", new CoordinatePair("-37.8212495", "145.0379136") },
            { "SPW", new CoordinatePair("-37.8210484", "145.0372979") },
            { "AS", new CoordinatePair("-37.8225553", "145.0373948") },
            { "AV", new CoordinatePair("-37.8231664", "145.0418801") },
            { "IS", new CoordinatePair("-37.8229283", "145.0419076") },
            { "AGSE", new CoordinatePair("-37.8213654", "145.0394651") },
            { "SR", new CoordinatePair("-37.8215099", "145.0387856") },
            { "TA", new CoordinatePair("-37.8210123", "145.0390028") },
            { "TB", new CoordinatePair("-37.8209596", "145.0373791") },
            { "TC", new CoordinatePair("-37.820569", "145.0374889") },
            { "TD", new CoordinatePair("-37.8206675", "145.0390926") },
            { "SA", new CoordinatePair("-37.8222579", "145.0368806") },
            { "CH", new CoordinatePair("-37.8226523", "145.0356731") }
        };

        public static string GetStaticMapURL(string buildingCode)
        {
            if (!SwinBuildings_HAW.ContainsKey(buildingCode)) return "Invalid Building Code";
            return $"http://maps.googleapis.com/maps/api/staticmap?center={SwinBuildings_HAW[buildingCode].XY}" +
                $"&zoom=18&size=400x400&maptype=roadmap&markers=size:medium|color:red|" +
                $"label:{buildingCode.Substring(0, 1)}|{SwinBuildings_HAW[buildingCode].XY}";
        }
    }

    public class CoordinatePair
    {
        public string X { get; set; }
        public string Y { get; set; }
        public string XY { get { return X + "," + Y; } }

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