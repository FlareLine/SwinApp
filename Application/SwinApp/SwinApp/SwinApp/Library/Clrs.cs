using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SwinApp.Library
{
    /// <summary>
    /// Clr defines all the colour names provided with the "Colors" implementation
    /// </summary>
    public enum Clr
    {
        Navy,
        Blue,
        Aqua,
        Teal,
        Olive,
        Green,
        Lime,
        Yellow,
        Orange,
        Red,
        Fuschia,
        Purple,
        Maroon,
        White,
        Silver,
        Gray,
        Black
    }

    /// <summary>
    /// Clrs provides static methods for working with the "Colors" implementation
    /// </summary>
    public static class Clrs
    {
        /// <summary>
        /// Retrieve a "Colors" shade of a color using the Clr enum
        /// </summary>
        /// <param name="clr">The color to retrieve</param>
        /// <returns></returns>
        public static Xamarin.Forms.Color FromEnum(Clr clr)
        {
            string val = clr.ToString();
            return (Xamarin.Forms.Color)Xamarin.Forms.Application.Current.Resources[val];
        }

        public static Dictionary<string, Color> timetableNameToColor = new Dictionary<string, Color>
        {
            { "Aqua", Color.FromHex("#00cccc") },
            { "Lilac", Color.FromHex("#ccccff") },
            { "Pink", Color.FromHex("#ff99cc") },
            { "Beige", Color.FromHex("#cccc99") },
            { "Green", Color.FromHex("#40bf80")},
            { "Pink-Purple", Color.FromHex("#e085c2")},
            { "Orange", Color.FromHex("#ffa64d")},
            { "Teal", Color.FromHex("#00cccc")},
            { "Pink-Red", Color.FromHex("#ff6666")}

        };
    }
}
