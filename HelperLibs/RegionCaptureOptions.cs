
namespace WinkingCat.HelperLibs
{
    public static class RegionCaptureOptions
    {
        public static InRegionTasks OnMouseMiddleClick { get; set; } = InRegionTasks.CaptureLastRegion;
        public static InRegionTasks OnMouseRightClick { get; set; } = InRegionTasks.RemoveSelectionOrCancel;
        public static InRegionTasks OnXButton1Click { get; set; } = InRegionTasks.CaptureActiveMonitor;
        public static InRegionTasks OnXButton2Click { get; set; } = InRegionTasks.CaptureFullScreen;
        public static InRegionTasks OnEscapePress { get; set; } = InRegionTasks.Cancel;
        public static InRegionTasks OnZPress { get; set; } = InRegionTasks.SwapCenterMagnifier;

        // magnifier
        public static bool DrawMagnifier { get; set; } = true;
        public static bool DrawCrosshairInMagnifier { get; set; } = true;
        public static bool DrawPixelGridInMagnifier { get; set; } = true;
        public static bool DrawBorderOnMagnifier { get; set; } = true;
        public static bool CenterMagnifierOnMouse { get; set; } = false;

        public static bool DrawBackgroundOverlay { get; set; } = true;
        public static bool updateOnMouseMove { get; set; } = true;
        public static bool DrawScreenWideCrosshair { get; set; } = true;
        public static bool DrawInfoText { get; set; } = true;
        public static bool DrawMarchingAnts { get; set; } = true;

        public static bool CreateClipAfterRegionCapture { get; set; } = false;
        public static bool AutoCopyImage { get; set; } = true;
        public static bool AutoCopyColor { get; set; } = true;

        public static float MagnifierZoomLevel { get; set; } = 1; // no more than 6 (Hard Coded Limit)
        public static float MagnifierZoomScale { get; set; } = 0.25f; // less = more scrolling, more = less scrolling
        public static int MagnifierPixelCount { get; set; } = 25; // needs to be odd number
        public static int MagnifierPixelSize { get; set; } = 6;  // needs to be odd number
        public static int CursorInfoOffset { get; set; } = 10;

        public static RegionCaptureMode Mode { get; set; } = RegionCaptureMode.Default;
    }
}
