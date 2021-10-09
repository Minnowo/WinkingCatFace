using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs
{
    public static class EnumHelper
    {
        public static string HashTypeToString(HashType ht)
        {
            switch (ht)
            {
                case HashType.CRC32:
                    return "CRC-32";
                case HashType.MD5:
                    return "MD5";
                case HashType.SHA1:
                    return "SHA-1";
                case HashType.SHA256:
                    return "SHA-256";
                case HashType.SHA384:
                    return "SHA-384";
                case HashType.SHA512:
                    return "SHA-512";
            }
            return string.Empty;
        }
    }
}
