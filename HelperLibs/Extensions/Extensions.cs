using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinkingCat.HelperLibs
{
    public static class Extensions
    {
        public static bool IsValid(this Rectangle rect)
        {
            return rect.Width > 0 && rect.Height > 0;
        }

        public static string Left(this string str, int length)
        {
            if (length < 1) return "";
            if (length < str.Length) return str.Substring(0, length);
            return str;
        }

        public static string Right(this string str, int length)
        {
            if (length < 1) return "";
            if (length < str.Length) return str.Substring(str.Length - length);
            return str;
        }

        public static string Truncate(this string str, int maxLength, string endings, bool truncateFromRight = true)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > maxLength)
            {
                int length = maxLength - endings.Length;

                if (length > 0)
                {
                    if (truncateFromRight)
                    {
                        str = str.Left(length) + endings;
                    }
                    else
                    {
                        str = endings + str.Right(length);
                    }
                }
            }

            return str;
        }
    }
}
