namespace WinkingCat.Controls
{
    public enum ZoomMode
    {
        /// <summary>
        /// Zooms into the top left corner of the image.
        /// </summary>
        TopLeftImage,

        /// <summary>
        /// Zooms into the bottom right of the image.
        /// </summary>
        BottomRightImage,

        /// <summary>
        /// Zooms into the ceneter of the image.
        /// </summary>
        CenterImage,

        /// <summary>
        /// CenterImage but at the mouse location.
        /// </summary>
        CenterMouse,

        /// <summary>
        /// Zooms into the pixel the mouse is on.
        /// </summary>
        IntoMouse
    }
}
