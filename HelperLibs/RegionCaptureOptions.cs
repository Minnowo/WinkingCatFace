
namespace WinkingCat.HelperLibs
{
    public static class RegionCaptureOptions
    {
        public static InRegionTasks onMouseMiddleClick { get; set; } = InRegionTasks.CaptureLastRegion;
        public static InRegionTasks onMouseRightClick { get; set; } = InRegionTasks.RemoveSelectionOrCancel;
        public static InRegionTasks onXButton1Click { get; set; } = InRegionTasks.CaptureActiveMonitor;
        public static InRegionTasks onXButton2Click { get; set; } = InRegionTasks.CaptureFullScreen;
        public static InRegionTasks onEscapePress { get; set; } = InRegionTasks.Cancel;
        public static InRegionTasks onZPress { get; set; } = InRegionTasks.SwapCenterMagnifier;

        // magnifier
        public static bool drawMagnifier { get; set; } = true;
        public static bool tryCenterMagnifier { get; set; } = false;
        public static bool drawMagnifierCrosshair { get; set; } = true;
        public static bool drawMagnifierGrid { get; set; } = true;
        public static bool drawMagnifierBorder { get; set; } = true;

        public static bool dimBackground { get; set; } = true;
        public static bool updateOnMouseMove { get; set; } = true;
        public static bool drawCrossHair { get; set; } = true;
        public static bool drawInfoText { get; set; } = true;
        public static bool marchingAnts 
        { 
            get 
            {
                return _marchingAnts;
            } 
            set 
            { 
                _marchingAnts = value;
                if (value)
                    useSolidCrossHair = false;
            } 
        }
        private static bool _marchingAnts = false;
        public static bool useSolidCrossHair { get; set; } = true;

        public static bool createSingleClipAfterRegionCapture { get; set; } = false;
        public static bool createClipAfterRegionCapture { get; set; } = false;
        public static bool autoCopyImage { get; set; } = true;
        public static bool autoCopyColor { get; set; } = true;

        public static float magnifierZoomLevel { get; set; } = 1; // no more than 6 (Hard Coded Limit)
        public static float magnifierZoomScale { get; set; } = 0.25f; // less = more scrolling, more = less scrolling
        public static int magnifierPixelCount { get; set; } = 25; // needs to be odd number
        public static int magnifierPixelSize { get; set; } = 6;  // needs to be odd number
        public static int cursorInfoOffset { get; set; } = 10;
        public static RegionCaptureMode mode { get; set; } = RegionCaptureMode.Default;
    }
}
