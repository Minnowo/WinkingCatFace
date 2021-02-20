
namespace WinkingCat.HelperLibs
{
    public static class RegionCaptureOptions
    {
       

        public static bool drawMagnifier { get; set; } = true;
        public static bool tryCenterMagnifier { get; set; } = false;
        public static bool drawCrossHair { get; set; } = true;
        public static bool drawInfoText { get; set; } = true;
        public static bool marchingAnts { get; set; } = true;

        public static bool createSingleClipAfterRegionCapture { get; set; } = false;
        public static bool createClipAfterRegionCapture { get; set; } = false;
        public static bool autoCopyImage { get; set; } = true;
        public static bool autoCopyColor { get; set; } = true;

        public static int magnifierZoomLevel { get; set; } = 2;
        public static int magnifierPixelCount = 30; 
        public static int magnifierPixelSize = 11;
        public static int cursorInfoOffset { get; set; } = 10;
        public static RegionCaptureMode mode { get; set; } = RegionCaptureMode.Default;
    }
}
