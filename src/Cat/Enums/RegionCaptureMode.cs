namespace WinkingCat.HelperLibs
{
    /// <summary>
    /// The different modes for the region capture.
    /// </summary>
    public enum RegionCaptureMode
    {
        /// <summary>
        /// The standard mode.
        /// </summary>
        Default,

        /// <summary>
        /// A more lightweight version of the standard mode.
        /// </summary>
        Light,

        /// <summary>
        /// A transparent selection window.
        /// </summary>
        Transparent,

        /// <summary>
        /// Only used for picking a color.
        /// </summary>
        ColorPicker
    }
}
