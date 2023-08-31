using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WinkingCat.Settings;


namespace WinkingCat.HelperLibs
{
    public static class Extensions
    {
        public static IEnumerable<T> OrderByNatural<T>(this IEnumerable<T> items, Func<T, string> selector, StringComparer stringComparer = null, bool ascendingOrder = true)
        {
            if (stringComparer == null)
            {
                stringComparer = StringComparer.CurrentCulture;
            }

            Regex regex = new Regex(@"\d+", RegexOptions.Compiled);

            int maxDigits = items
                          .SelectMany(i => regex.Matches(selector(i)).Cast<Match>().Select(digitChunk => (int?)digitChunk.Value.Length))
                          .Max() ?? 0;

            if (ascendingOrder)
                return items.OrderBy(i => regex.Replace(selector(i), match => match.Value.PadLeft(maxDigits, '0')), stringComparer);

            return items.OrderByDescending(i => regex.Replace(selector(i), match => match.Value.PadLeft(maxDigits, '0')), stringComparer);
        }



        public static Bitmap Copy(this Image image)
        {
            return ImageProcessor.DeepClone(image, PixelFormat.Format32bppArgb);
        }

        public static void ShowError(this Exception e)
        {
            MessageBox.Show(null, e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public static void InvokeSafe(this Control control, Action action)
        {
            if (control == null || control.IsDisposed)
                return;

            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
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
            catch { }

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



        public static Rectangle SizeOffset(this Rectangle rect, int width, int height)
        {
            return new Rectangle(rect.X, rect.Y, rect.Width + width, rect.Height + height);
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
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

        public static void SupportCustomTheme(this ListView lv)
        {
            if (lv.OwnerDraw)
                return;

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
                using (Brush brush = new SolidBrush(SettingsManager.MainFormSettings.backgroundColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                TextRenderer.DrawText(e.Graphics, e.Header.Text, e.Font, e.Bounds.LocationOffset(2, 0).SizeOffset(-4, 0),
                    SettingsManager.MainFormSettings.textColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                if (e.Bounds.Right < lv.ClientRectangle.Right)
                {
                    using (Pen pen = new Pen(SettingsManager.MainFormSettings.separatorDarkColor))
                    using (Pen pen2 = new Pen(SettingsManager.MainFormSettings.separatorLightColor))
                    {
                        e.Graphics.DrawLine(pen, e.Bounds.Right - 2, e.Bounds.Top, e.Bounds.Right - 2, e.Bounds.Bottom - 1);
                        e.Graphics.DrawLine(pen2, e.Bounds.Right - 1, e.Bounds.Top, e.Bounds.Right - 1, e.Bounds.Bottom - 1);
                    }
                }
            };
        }
    }
}
