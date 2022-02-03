using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WinkingCat.HelperLibs;

namespace WinkingCat.HelperLibs
{
    public class ClipOptions
    {
        public string Name;
        public string FilePath;

        public int BorderThickness;
        public int BorderGrabSize;
        public int ZoomRefreshRate;

        public DateTime DateCreated;
        
        public Color Color;

        public Point Location;

        public ClipOptions()
        {
            DateCreated = DateTime.Now;

            Name = string.Format("{0}--{1}", Guid.NewGuid().ToString(), DateCreated);

            Color = SettingsManager.ClipSettings.Border_Color;
            BorderThickness = SettingsManager.ClipSettings.Border_Thickness;
            BorderGrabSize = SettingsManager.ClipSettings.Border_Grab_Size;
            ZoomRefreshRate = SettingsManager.ClipSettings.Zoom_Refresh_Rate;
        }

        public ClipOptions(Point locataion) : this()
        {
            Location = locataion;
        }
    }
}
