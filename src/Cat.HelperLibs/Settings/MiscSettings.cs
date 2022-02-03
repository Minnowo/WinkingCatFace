using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace WinkingCat.HelperLibs
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MiscSettings
    {
        public string Screenshot_Folder_Path { get; set; } = "";
        public string Browser_Path { get; set; } = "";
        public bool Use_Custom_Screenshot_Folder { get; set; } = false;
        public bool Save_Images_To_Disk 
        { 
            get 
            { 
                return InternalSettings.Save_Images_To_Disk; 
            } 
            set 
            {
                InternalSettings.Save_Images_To_Disk = value;
            } 
        }

        public bool Prefer_DWRM_Over_WRM
        {
            get
            {
                return InternalSettings.Save_WORM_As_DWORM;
            }
            set
            {
                InternalSettings.Save_WORM_As_DWORM = value;
            }
        }
        public ImgFormat Default_Image_Format { get; set; } = ImgFormat.png;
        public ColorFormat Default_Color_Format { get; set; } = ColorFormat.RGB;
        public InterpolationMode Default_Interpolation_Mode { get; set; } = InterpolationMode.NearestNeighbor;
        public HelperLibs.Controls.DrawMode Default_Draw_Mode { get; set; } = Controls.DrawMode.FitImage;
    }
}
