using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    /// <summary>
    /// The results from the region capture form.
    /// </summary>
    public enum RegionResult
    {
        /// <summary>
        /// The region capture was closed without returning anything.
        /// </summary>
        Close,

        /// <summary>
        /// A region was captured.
        /// </summary>
        Region,

        /// <summary>
        /// The last region was captured.
        /// </summary>
        LastRegion,

        /// <summary>
        /// All monitors were captured.
        /// </summary>
        Fullscreen,

        /// <summary>
        /// A single monitor was captured.
        /// </summary>
        Monitor,

        /// <summary>
        /// The active monitor was captured.
        /// </summary>
        ActiveMonitor,

        /// <summary>
        /// A color was captured.
        /// </summary>
        Color
    }
}
