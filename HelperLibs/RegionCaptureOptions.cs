
namespace WinkingCat.HelperLibs
{
    public static class RegionCaptureOptions
    {
       

        public static bool drawMagnifier { get; set; } = true;
        public static bool drawCrossHair { get; set; } = true;
        public static bool drawInfoText { get; set; } = true;
        public static bool marchingAnts { get; set; } = true;

        public static bool createSingleClipAfterRegionCapture { get; set; } = false;
        public static bool createClipAfterRegionCapture { get; set; } = false;
        public static bool autoCopyImage { get; set; } = true;
        public static bool autoCopyColor { get; set; } = true;

        public static int MagnifierPixelCount = 11; 
        public static int MagnifierPixelSize = 11;
        public static int cursorInfoOffset { get; set; } = 10;
        public static RegionCaptureMode mode { get; set; } = RegionCaptureMode.Default;
    }
}
