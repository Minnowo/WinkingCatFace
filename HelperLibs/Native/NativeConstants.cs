using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    public static class NativeConstants
    {
        public const int CURSOR_SHOWING = 1;
        public const int GCL_HICONSM = -34;
        public const int GCL_HICON = -14;
        public const int ICON_SMALL = 0;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL2 = 2;
        public const int GWL_STYLE = -16;
        public const long WS_EX_TOPMOST = 0x00000008L;
        /// <summary>
        /// Combination of DI_IMAGE and DI_MASK.
        /// </summary>
        public const int DI_NORMAL = 0x0003;
    }
}
