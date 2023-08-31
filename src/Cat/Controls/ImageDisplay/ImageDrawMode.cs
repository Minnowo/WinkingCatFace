namespace WinkingCat.Controls
{
    public enum ImageDrawMode
    {
        /// <summary>
        /// Always scale the image to fit the maximum possible size.
        /// </summary>
        FitImage,

        /// <summary>
        /// Only show the image as default size.
        /// </summary>
        ActualSize,

        /// <summary>
        /// Scale the image when it doesn't fit on the control, otherwise show the default image size
        /// </summary>
        DownscaleImage,

        /// <summary>
        /// Allows for free dragging and zooming of the image.
        /// </summary>
        Resizeable
    }
}
