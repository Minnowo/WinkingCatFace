using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    /// <summary>
    /// The location of a tooltip relative to a control.
    /// </summary>
    public enum ToolTipLocation
    {
        /// <summary>
        /// On the mouse.
        /// </summary>
        Mouse,

        /// <summary>
        /// Above the control.
        /// </summary>
        ControlTop,

        /// <summary>
        /// Below the control.
        /// </summary>
        ControlBottom,

        /// <summary>
        /// To the left of the control.
        /// </summary>
        ControlLeft,

        /// <summary>
        /// To the right of the control.
        /// </summary>
        ControlRight
    }
}
