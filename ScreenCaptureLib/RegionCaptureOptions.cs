
namespace WinkingCat.ScreenCaptureLib
{
    public static class RegionCaptureOptions
    {
        public static int cursorInfoOffset { get; set; } = 10;

        public static bool drawMagnifier { get; set; } = false;
        public static bool drawCrossHair { get; set; } = true;
        public static bool drawInfoText { get; set; } = true;
        public static bool marchingAnts { get; set; } = true;

        public static bool createSingleClipAfterRegionCapture { get; set; } = false;
        public static bool createClipAfterRegionCapture { get; set; } = false;

        public static int MagnifierPixelCount = 15; // Must be odd number like 11, 13, 15 etc.
        public static int MagnifierPixelSize = 10;

        public static RegionCaptureMode mode { get; set; } = RegionCaptureMode.Default;
    }
}
