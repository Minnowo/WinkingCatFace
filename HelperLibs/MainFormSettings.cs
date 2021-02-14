using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    public static class MainFormSettings
    {
        public static bool hideMainFormOnCapture { get; set; } = true;
        public static int waitHideTime { get; set; } = 500;

        public static bool showInTray { get; set; } = true;
    }
}
