using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    /// <summary>
    /// The different hotkey status.
    /// </summary>
    public enum HotkeyStatus
    {
        /// <summary>
        /// The hotkey was registered without issue.
        /// </summary>
        Registered,

        /// <summary>
        /// The hotkey was not registered for X reason.
        /// </summary>
        Failed,

        /// <summary>
        /// The hotkey has not been set.
        /// </summary>
        NotSet
    }
}
