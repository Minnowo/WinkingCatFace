using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinkingCat.Settings;
using WinkingCat.HelperLibs;
namespace WinkingCat
{
    public static class Helper
    {
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
