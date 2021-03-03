using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public class AppstyleTEST
    {

        #region Main Form Themes
        public string mainFormName { get; set; } = "";
        public float contextMenuOpacity { get; set; } = 0.9f;
        public bool useImersiveDarkMode { get; set; } = true;
        public Color backgroundColor { get; set; } = Color.FromArgb(42, 47, 56);
        public Color lightBackgroundColor { get; set; } = Color.FromArgb(52, 57, 65);
        public Color darkBackgroundColor { get; set; } = Color.FromArgb(28, 32, 38);
        public Color textColor { get; set; } = Color.FromArgb(235, 235, 235);
        public static Color borderColor { get; set; } = Color.FromArgb(28, 32, 38);
        public static Color menuHighlightColor { get; set; } = Color.FromArgb(30, 34, 40);
        public static Color menuHighlightBorderColor { get; set; } = Color.FromArgb(116, 129, 152);
        public static Color menuBorderColor { get; set; } = Color.FromArgb(22, 26, 31);
        public static Color menuCheckBackgroundColor { get; set; } = Color.FromArgb(26, 64, 75);
        public static Color separatorDarkColor { get; set; } = Color.FromArgb(22, 26, 31);
        public static Color separatorLightColor { get; set; } = Color.FromArgb(56, 64, 75);
        public static Color contextMenuFontColor { get; set; } = Color.White;

        #endregion

        #region Region Capture Themes
        public static Color BackgroundOverlayColor
        {
            get
            {
                return backgroundOverlayColor;
            }
            set
            {
                backgroundOverlayOpacity = value.A;
                backgroundOverlayColor = value;
            }
        }
        private static Color backgroundOverlayColor = Color.Black;
        public static ushort BackgroundOverlayOpacity
        {
            get
            {
                return backgroundOverlayOpacity;
            }
            set
            {
                if (value != backgroundOverlayOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    backgroundOverlayOpacity = newVal;
                    backgroundOverlayColor = Color.FromArgb(newVal,
                        backgroundOverlayColor.R,
                        backgroundOverlayColor.G,
                        backgroundOverlayColor.B);
                }
            }
        }
        private static ushort backgroundOverlayOpacity = 36;

        private static Color ScreenWideCrosshairColor
        {
            get
            {
                return screenWideCrosshairColor;
            }
            set
            {
                screenWideCrosshairColor = value;
            }
        }
        public static Color screenWideCrosshairColor = Color.FromArgb(249, 0, 187);
        public static ushort ScreenWideCrosshairOpacity
        {
            get
            {
                return screenWideCrosshairOpacity;
            }
            set
            {
                if (value != screenWideCrosshairOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    screenWideCrosshairOpacity = newVal;
                    screenWideCrosshairColor = Color.FromArgb(newVal,
                        screenWideCrosshairColor.R,
                        screenWideCrosshairColor.G,
                        screenWideCrosshairColor.B);
                }
            }
        }
        private static ushort screenWideCrosshairOpacity = 255;

        private static Color MagnifierCrosshairColor
        {
            get
            {
                return magnifierCrosshairColor;
            }
            set
            {
                magnifierCrosshairOpacity = value.A;
                magnifierCrosshairColor = value;
            }
        }
        public static Color magnifierCrosshairColor = Color.LightBlue;
        public static ushort MagnifierCrosshairOpacity
        {
            get
            {
                return magnifierCrosshairOpacity;
            }
            set
            {
                if (value != magnifierCrosshairOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    magnifierCrosshairOpacity = newVal;
                    magnifierCrosshairColor = Color.FromArgb(newVal,
                        magnifierCrosshairColor.R,
                        magnifierCrosshairColor.G,
                        magnifierCrosshairColor.B);
                }
            }
        }
        private static ushort magnifierCrosshairOpacity = 125;

        public static Color MagnifierGridColor
        {
            get
            {
                return magnifierGridColor;
            }
            set
            {
                magnifierGridOpacity = value.A;
                magnifierGridColor = value;
            }
        }
        private static Color magnifierGridColor = Color.Black;
        public static ushort MagnifierGridOpacity
        {
            get
            {
                return magnifierGridOpacity;
            }
            set
            {
                if (value != magnifierGridOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    magnifierGridOpacity = newVal;
                    magnifierGridColor = Color.FromArgb(newVal,
                        magnifierGridColor.R,
                        magnifierGridColor.G,
                        magnifierGridColor.B);
                }
            }
        }
        private static ushort magnifierGridOpacity = 255;

        public static Color MagnifierBorderColor
        {
            get
            {
                return magnifierBorderColor;
            }
            set
            {
                mangifierBorderOpacity = value.A;
                magnifierBorderColor = value;
            }
        }
        private static Color magnifierBorderColor = Color.Black;
        public static ushort MangifierBorderOpacity
        {
            get
            {
                return mangifierBorderOpacity;
            }
            set
            {
                if (value != mangifierBorderOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    mangifierBorderOpacity = newVal;
                    magnifierBorderColor = Color.FromArgb(newVal,
                        magnifierBorderColor.R,
                        magnifierBorderColor.G,
                        magnifierBorderColor.B);
                }
            }
        }
        private static ushort mangifierBorderOpacity = 255;

        public static Color infoTextBackgroundColor { get; set; } = Color.FromArgb(39, 43, 50);
        public static Color infoTextBorderColor { get; set; } = Color.Black;
        public static Color infoTextTextColor { get; set; } = Color.FromArgb(255, 255, 255);
        #endregion

        #region Clip Themes
        public static ushort clipContextMenuOpacity { get; set; } = 100;

        public static Color clipBorderColor { get; set; } = Color.FromArgb(249, 0, 187);
        public static Color clipMenuHighlightColor { get; set; } = Color.FromArgb(30, 34, 40);
        public static Color clipMenuHighlightBorderColor { get; set; } = Color.FromArgb(116, 129, 152);
        public static Color clipMenuBorderColor { get; set; } = Color.FromArgb(22, 26, 31);
        public static Color clipMenuCheckBackgroundColor { get; set; } = Color.FromArgb(56, 64, 75);
        #endregion

    }
    public static class ApplicationStyles
    {
        public static event EventHandler UpdateSylesEvent;

        #region Main Form Themes
        public static string mainFormName { get; set; } = "";
        public static float contextMenuOpacity { get; set; } = 0.9f;
        public static bool useImersiveDarkMode { get; set; } = true;
        public static Color backgroundColor { get; set; } = Color.FromArgb(42, 47, 56);
        public static Color lightBackgroundColor { get; set; } = Color.FromArgb(52, 57, 65);
        public static Color darkBackgroundColor { get; set; } = Color.FromArgb(28, 32, 38);
        public static Color textColor { get; set; } = Color.FromArgb(235, 235, 235);
        public static Color borderColor { get; set; } = Color.FromArgb(28, 32, 38);
        public static Color menuHighlightColor { get; set; } = Color.FromArgb(30, 34, 40);
        public static Color menuHighlightBorderColor { get; set; } = Color.FromArgb(116, 129, 152);
        public static Color menuBorderColor { get; set; } = Color.FromArgb(22, 26, 31);
        public static Color menuCheckBackgroundColor { get; set; } = Color.FromArgb(26, 64, 75);
        public static Color separatorDarkColor { get; set; } = Color.FromArgb(22, 26, 31);
        public static Color separatorLightColor { get; set; } = Color.FromArgb(56, 64, 75);
        public static Color contextMenuFontColor { get; set; } = Color.White;

        #endregion

        #region Region Capture Themes
        public static Color BackgroundOverlayColor
        {
            get
            {
                return backgroundOverlayColor;
            }
            set
            {
                backgroundOverlayOpacity = value.A;
                backgroundOverlayColor = value;
            }
        }
        private static Color backgroundOverlayColor = Color.Black;
        public static ushort BackgroundOverlayOpacity
        {
            get
            {
                return backgroundOverlayOpacity;
            }
            set
            {
                if (value != backgroundOverlayOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    backgroundOverlayOpacity = newVal;
                    backgroundOverlayColor = Color.FromArgb(newVal,
                        backgroundOverlayColor.R,
                        backgroundOverlayColor.G,
                        backgroundOverlayColor.B);
                }
            }
        }
        private static ushort backgroundOverlayOpacity = 36;

        private static Color ScreenWideCrosshairColor
        {
            get
            {
                return screenWideCrosshairColor;
            }
            set
            {
                screenWideCrosshairColor = value;
            }
        }
        public static Color screenWideCrosshairColor = Color.FromArgb(249, 0, 187);
        public static ushort ScreenWideCrosshairOpacity
        {
            get
            {
                return screenWideCrosshairOpacity;
            }
            set
            {
                if (value != screenWideCrosshairOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    screenWideCrosshairOpacity = newVal;
                    screenWideCrosshairColor = Color.FromArgb(newVal,
                        screenWideCrosshairColor.R,
                        screenWideCrosshairColor.G,
                        screenWideCrosshairColor.B);
                }
            }
        }
        private static ushort screenWideCrosshairOpacity = 255;

        private static Color MagnifierCrosshairColor
        {
            get
            {
                return magnifierCrosshairColor;
            }
            set
            {
                magnifierCrosshairOpacity = value.A;
                magnifierCrosshairColor = value;
            }
        }
        public static Color magnifierCrosshairColor = Color.LightBlue;
        public static ushort MagnifierCrosshairOpacity
        {
            get
            {
                return magnifierCrosshairOpacity;
            }
            set
            {
                if (value != magnifierCrosshairOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    magnifierCrosshairOpacity = newVal;
                    magnifierCrosshairColor = Color.FromArgb(newVal,
                        magnifierCrosshairColor.R,
                        magnifierCrosshairColor.G,
                        magnifierCrosshairColor.B);
                }
            }
        }
        private static ushort magnifierCrosshairOpacity = 125;

        public static Color MagnifierGridColor
        {
            get
            {
                return magnifierGridColor;
            }
            set
            {
                magnifierGridOpacity = value.A;
                magnifierGridColor = value;
            }
        }
        private static Color magnifierGridColor = Color.Black;
        public static ushort MagnifierGridOpacity
        {
            get
            {
                return magnifierGridOpacity;
            }
            set
            {
                if (value != magnifierGridOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    magnifierGridOpacity = newVal;
                    magnifierGridColor = Color.FromArgb(newVal,
                        magnifierGridColor.R,
                        magnifierGridColor.G,
                        magnifierGridColor.B);
                }
            }
        }
        private static ushort magnifierGridOpacity = 255;

        public static Color MagnifierBorderColor
        {
            get
            {
                return magnifierBorderColor;
            }
            set
            {
                mangifierBorderOpacity = value.A;
                magnifierBorderColor = value;
            }
        }
        private static Color magnifierBorderColor = Color.Black;
        public static ushort MangifierBorderOpacity
        {
            get
            {
                return mangifierBorderOpacity;
            }
            set
            {
                if (value != mangifierBorderOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    mangifierBorderOpacity = newVal;
                    magnifierBorderColor = Color.FromArgb(newVal,
                        magnifierBorderColor.R,
                        magnifierBorderColor.G,
                        magnifierBorderColor.B);
                }
            }
        }
        private static ushort mangifierBorderOpacity = 255;

        public static Color infoTextBackgroundColor { get; set; } = Color.FromArgb(39, 43, 50);
        public static Color infoTextBorderColor { get; set; } = Color.Black;
        public static Color infoTextTextColor { get; set; } = Color.FromArgb(255, 255, 255);
        #endregion

        #region Clip Themes
        public static ushort clipContextMenuOpacity { get; set; } = 100;

        public static Color clipBorderColor { get; set; } = Color.FromArgb(249, 0, 187);
        public static Color clipMenuHighlightColor { get; set; } = Color.FromArgb(30, 34, 40);
        public static Color clipMenuHighlightBorderColor { get; set; } = Color.FromArgb(116, 129, 152);
        public static Color clipMenuBorderColor { get; set; } = Color.FromArgb(22, 26, 31);
        public static Color clipMenuCheckBackgroundColor { get; set; } = Color.FromArgb(56, 64, 75);
        #endregion



        public static void UpdateAll()
        {
            OnUpdateEvent();
        }

        private static void OnUpdateEvent()
        {
            if (UpdateSylesEvent != null)
            {
                UpdateSylesEvent(null, EventArgs.Empty);
            }
        }
    }

    public class ToolStripCustomRenderer : ToolStripProfessionalRenderer
    {
        public ToolStripCustomRenderer() : base(new CustomColorTable())
        {
            RoundedEdges = false;
        }

        public ToolStripCustomRenderer(CustomColorTable customColorTable) : base(customColorTable)
        {
            RoundedEdges = false;
        }


        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item is ToolStripMenuItem tsmi && tsmi.Checked)
            {
                e.TextFont = new Font(tsmi.Font, FontStyle.Bold);
            }
            e.Item.ForeColor = ApplicationStyles.textColor;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (e.Item is ToolStripDropDownButton tsddb && tsddb.Owner is ToolStrip)
            {
                e.Direction = ArrowDirection.Right;
            }
            e.ArrowColor = ApplicationStyles.textColor;
            base.OnRenderArrow(e);
        }
    }

    public class CustomColorTable : ProfessionalColorTable
    {
        public override Color ButtonSelectedHighlight
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color ButtonSelectedHighlightBorder
        {
            get { return ApplicationStyles.menuHighlightBorderColor; }
        }
        public override Color ButtonPressedHighlight
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color ButtonPressedHighlightBorder
        {
            get { return ApplicationStyles.menuHighlightBorderColor; }
        }
        public override Color ButtonCheckedHighlight
        {
            get { return ApplicationStyles.menuCheckBackgroundColor; }
        }
        public override Color ButtonCheckedHighlightBorder
        {
            get { return ApplicationStyles.menuHighlightBorderColor; }
        }
        public override Color ButtonPressedBorder
        {
            get { return ApplicationStyles.menuHighlightBorderColor; }
        }
        public override Color ButtonSelectedBorder
        {
            get { return ApplicationStyles.menuHighlightBorderColor; }
        }
        public override Color ButtonCheckedGradientBegin
        {
            get { return ApplicationStyles.menuCheckBackgroundColor; }
        }
        public override Color ButtonCheckedGradientMiddle
        {
            get { return ApplicationStyles.menuCheckBackgroundColor; }
        }
        public override Color ButtonCheckedGradientEnd
        {
            get { return ApplicationStyles.menuCheckBackgroundColor; }
        }
        public override Color ButtonSelectedGradientBegin
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color ButtonSelectedGradientMiddle
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color ButtonSelectedGradientEnd
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color ButtonPressedGradientBegin
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color ButtonPressedGradientMiddle
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color ButtonPressedGradientEnd
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color CheckBackground
        {
            get { return ApplicationStyles.menuCheckBackgroundColor; }
        }
        public override Color CheckSelectedBackground
        {
            get { return ApplicationStyles.menuCheckBackgroundColor; }
        }
        public override Color CheckPressedBackground
        {
            get { return ApplicationStyles.menuCheckBackgroundColor; }
        }
        public override Color GripDark
        {
            get { return ApplicationStyles.separatorDarkColor; }
        }
        public override Color GripLight
        {
            get { return ApplicationStyles.separatorLightColor; }
        }
        public override Color ImageMarginGradientBegin
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ImageMarginGradientMiddle
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ImageMarginGradientEnd
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ImageMarginRevealedGradientBegin
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ImageMarginRevealedGradientMiddle
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ImageMarginRevealedGradientEnd
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color MenuStripGradientBegin
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color MenuStripGradientEnd
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color MenuItemSelected
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color MenuItemBorder
        {
            get { return ApplicationStyles.menuBorderColor; }
        }
        public override Color MenuBorder
        {
            get { return ApplicationStyles.menuBorderColor; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color MenuItemPressedGradientMiddle
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return ApplicationStyles.menuHighlightColor; }
        }
        public override Color RaftingContainerGradientBegin
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color RaftingContainerGradientEnd
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color SeparatorDark
        {
            get { return ApplicationStyles.separatorDarkColor; }
        }
        public override Color SeparatorLight
        {
            get { return ApplicationStyles.separatorLightColor; }
        }
        public override Color StatusStripGradientBegin
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color StatusStripGradientEnd
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ToolStripBorder
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ToolStripGradientBegin
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ToolStripGradientMiddle
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ToolStripGradientEnd
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ToolStripContentPanelGradientBegin
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ToolStripContentPanelGradientEnd
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ToolStripPanelGradientBegin
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color ToolStripPanelGradientEnd
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color OverflowButtonGradientBegin
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color OverflowButtonGradientMiddle
        {
            get { return ApplicationStyles.backgroundColor; }
        }
        public override Color OverflowButtonGradientEnd
        {
            get { return ApplicationStyles.backgroundColor; }
        }
    }
}
