namespace SwinApp.Library
{
    public interface IOrientationProvider
    {
        /// <summary>
        /// Set orientation to Horizontal
        /// </summary>
        void Landscape();

        /// <summary>
        /// Set the orientation to portrait
        /// </summary>
        void Portrait();

        /// <summary>
        /// Get the current orientation
        /// </summary>
        /// <returns></returns>
        Orientation Current();
    }
}
