namespace WinkingCat.HelperLibs
{
    public enum Function
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
        Regex,
        QRCode,
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
        SwapCenterMagnifier
    }

}
