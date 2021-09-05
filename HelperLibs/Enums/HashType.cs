using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    /// <summary>
    /// Represents the different supported hash algorithms.
    /// </summary>
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
}
