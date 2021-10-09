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
        CRC32,
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
}
