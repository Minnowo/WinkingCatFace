using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinkingCat.HelperLibs.Enums
{
    public static class EnumToString
    {
        public static string FileSizeUnitToString(FileSizeUnit fsu)
        {
            switch (fsu)
            {
                case FileSizeUnit.Byte:
                    return "Byte";
                case FileSizeUnit.Kilobyte:
                    return "Kilobyte";
                case FileSizeUnit.Megabyte: 
                    return "Megabyte";
                case FileSizeUnit.Gigabyte:
                    return "Gigabyte";
                case FileSizeUnit.Terabyte:
                    return "Terabyte";
                case FileSizeUnit.Petabyte:
                    return "Petabyte";
                case FileSizeUnit.Exabyte:
                    return "Exabyte";
                case FileSizeUnit.Zettabyte:
                    return "Zettabyte";
            }
            return string.Empty;
        }
    }
}
