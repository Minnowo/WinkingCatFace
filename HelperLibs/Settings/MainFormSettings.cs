using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MainFormSettings
    {
        public bool Show_In_Tray { get; set; } = true;
        public bool Start_In_Tray { get; set; } = false;
        public bool Hide_In_Tray_On_Close { get; set; } = true;
        public bool Hide_Form_On_Captrue { get; set; } = true;
        public bool Always_On_Top { get; set; } = true;

        public int Wait_Hide_Time { get; set; } = 300;
        public int Image_Counter { get; set; } = 0;

        public Function On_Tray_Left_Click { get; set; } = Function.RegionCapture;
        public Function On_Tray_Double_Click { get; set; } = Function.OpenMainForm;
        public Function On_Tray_Middle_Click { get; set; } = Function.NewClipFromClipboard;

        public static void UpdateSettings()
        {

        }
    }
}
