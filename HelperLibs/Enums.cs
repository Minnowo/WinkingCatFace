

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
        CaptureWindow,
        CaptureGif,
        NewOCRCapture,
        ColorWheelPicker,
        HashCheck,
        OpenMainForm
    }

    public enum ColorFormat
    {
        ARGB,
        Hex,
        Decminal,
        CMYK,
        HSB
    }
}
