

using System.ComponentModel;

namespace WinkingCat.HelperLibs
{
    public enum DrawStyles
    {
        Red,
        Green,
        Blue,
        HSBHue,
        HSBSaturation,
        HSBBrightness,
        HSLHue,
        HSLSaturation,
        HSLLightness,
        xyz
    }

    public enum HashType
    {
        [Description("CRC-32")]
        CRC32,
        [Description("CRC-64 (ECMA-182)")]
        CRC64,
        [Description("MD5")]
        MD5,
        [Description("SHA-1")]
        SHA1,
        [Description("SHA-256")]
        SHA256,
        [Description("SHA-384")]
        SHA384,
        [Description("SHA-512")]
        SHA512
    }

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
        None,
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

    public enum InRegionTasks
    {
        DoNothing,
        Cancel,
        RemoveSelectionOrCancel,
        CaptureFullScreen,
        CaptureActiveMonitor,
        CaptureLastRegion,
        RemoveSelection,
        SwapToolType,
        SwapCenterMagnifier
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
        AdobeRGB
    }
}
