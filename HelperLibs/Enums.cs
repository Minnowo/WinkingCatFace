

namespace WinkingCat.HelperLibs
{

    public enum RegionCaptureMode
    {
        Default,
        Light,
        Transparent,
        ColorPicker
    }

    public enum RegionResult
    {
        Close,
        Region,
        LastRegion,
        Fullscreen,
        Monitor,
        ActiveMonitor,
        Color
    }
    public enum HotkeyStatus
    {
        Registered,
        Failed,
        NotSet
    }
    public enum Tasks
    {
        RegionCapture,
        RegionCaptureLite,
        NewClipFromRegionCapture,
        NewClipFromFile,
        NewClipFromClipboard,
        ScreenColorPicker,
        CaptureLastRegion,
        CaptureFullScreen,
        CaptureActiveMonitor,
        CaptureActiveWindow,
        CaptureGif,
        NewOCRCapture,
        ColorWheelPicker,
        HashCheck,
        OpenMainForm
    }

    public enum ColorFormat
    {
        RGB,
        ARGB,
        Hex,
        Decminal,
        CMYK,
        HSB,
        HSV,
        HSL,
        XYZ,
        Yxy,
        AdobeRGB,
        All
    }
}
