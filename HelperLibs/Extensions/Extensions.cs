using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace WinkingCat.HelperLibs
{
    public static class Extensions
    {
        public static string GetHash<T>(this string str) where T : HashAlgorithm, new()
        {
            using (T crypt = new T())
            {
                return ReturnStrHash(crypt.ComputeHash(Encoding.UTF8.GetBytes(str)));
            }
        }

        public static string GetHash<T>(this Stream stream) where T : HashAlgorithm, new() 
        { 
            using (T crypt = new T()) 
            { 
                return ReturnStrHash(crypt.ComputeHash(stream)); 
            }
        }

        public static string ReturnStrHash(byte[] crypto)
        {
            StringBuilder hash = new System.Text.StringBuilder();
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public static T CloneSafe<T>(this T obj) where T : class, ICloneable
        {
            try
            {
                if (obj != null)
                {
                    return obj.Clone() as T;
                }
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
            }

            return null;
        }


        public static Rectangle LocationOffset(this Rectangle rect, int x, int y)
        {
            return new Rectangle(rect.X + x, rect.Y + y, rect.Width, rect.Height);
        }

        public static void ForceActivate(this Form form)
        {
            if (!form.IsDisposed)
            {
                if (!form.Visible)
                {
                    form.Show();
                }

                if (form.WindowState == FormWindowState.Minimized)
                {
                    form.WindowState = FormWindowState.Normal;
                }

                form.BringToFront();
                form.Activate();
            }
        }

        public static void SupportCustomTheme(this ListView lv)
        {
            if (!lv.OwnerDraw)
            {
                lv.OwnerDraw = true;

                lv.DrawItem += (sender, e) =>
                {
                    e.DrawDefault = true;
                };

                lv.DrawSubItem += (sender, e) =>
                {
                    e.DrawDefault = true;
                };

                lv.DrawColumnHeader += (sender, e) =>
                {
                    using (Brush brush = new SolidBrush(ApplicationStyles.currentStyle.mainFormStyle.backgroundColor))
                    {
                        e.Graphics.FillRectangle(brush, e.Bounds);
                    }

                    TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, e.Bounds.LocationOffset(2, 0).SizeOffset(-4, 0),
                        ApplicationStyles.currentStyle.mainFormStyle.textColor,
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                    if (e.Bounds.Right < lv.ClientRectangle.Right)
                    {
                        using (Pen pen = new Pen(ApplicationStyles.currentStyle.mainFormStyle.separatorDarkColor))
                        using (Pen pen2 = new Pen(ApplicationStyles.currentStyle.mainFormStyle.separatorLightColor))
                        {
                            e.Graphics.DrawLine(pen, e.Bounds.Right - 2, e.Bounds.Top, e.Bounds.Right - 2, e.Bounds.Bottom - 1);
                            e.Graphics.DrawLine(pen2, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
                        }
                    }
                };
            }
        }

        public static Rectangle SizeOffset(this Rectangle rect, int width, int height)
        {
            return new Rectangle(rect.X, rect.Y, rect.Width + width, rect.Height + height);
        }

        public static T Clamp<T>(this T input, T min, T max) where T : IComparable<T>
        {
            return MathHelper.Clamp(input, min, max);
        }
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }

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
