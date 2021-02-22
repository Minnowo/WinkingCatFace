
namespace WinkingCat.HelperLibs
{
    public static class RegionCaptureOptions
    {
       

        public static bool drawMagnifier { get; set; } = true;
        public static bool tryCenterMagnifier { get; set; } = true;
        public static bool drawMagnifierCrosshair { get; set; } = true;
        public static bool drawMagnifierGrid { get; set; } = true;
        public static bool drawCrossHair { get; set; } = true;
        public static bool drawInfoText { get; set; } = true;
        public static bool marchingAnts { get; set; } = true;

        public static bool createSingleClipAfterRegionCapture { get; set; } = false;
        public static bool createClipAfterRegionCapture { get; set; } = false;
        public static bool autoCopyImage { get; set; } = true;
        public static bool autoCopyColor { get; set; } = true;

        public static float magnifierZoomLevel { get; set; } = 2; // no more than 6 (Hard Coded Limit)
        public static float magnifierZoomScale { get; set; } = 0.25f; // less = more scrolling, more = less scrolling
        public static int magnifierPixelCount = 25; // needs to be odd number
        public static int magnifierPixelSize = 11;  // needs to be odd number
        public static int cursorInfoOffset { get; set; } = 10;
        public static RegionCaptureMode mode { get; set; } = RegionCaptureMode.Default;
    }
}
