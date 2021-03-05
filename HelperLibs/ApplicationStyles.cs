using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public class MainFormStyle
    {
        #region Main Form Themes
        public float contextMenuOpacity { get; set; } = 0.9f;
        public bool useImersiveDarkMode { get; set; } = true;
        public Color backgroundColor { get; set; } = Color.FromArgb(42, 47, 56);
        public Color lightBackgroundColor { get; set; } = Color.FromArgb(52, 57, 65);
        public Color darkBackgroundColor { get; set; } = Color.FromArgb(28, 32, 38);
        public Color textColor { get; set; } = Color.FromArgb(235, 235, 235);
        public Color borderColor { get; set; } = Color.FromArgb(28, 32, 38);
        public Color menuHighlightColor { get; set; } = Color.FromArgb(30, 34, 40);
        public Color menuHighlightBorderColor { get; set; } = Color.FromArgb(116, 129, 152);
        public Color menuBorderColor { get; set; } = Color.FromArgb(22, 26, 31);
        public Color menuCheckBackgroundColor { get; set; } = Color.FromArgb(26, 64, 75);
        public Color separatorDarkColor { get; set; } = Color.FromArgb(22, 26, 31);
        public Color separatorLightColor { get; set; } = Color.FromArgb(56, 64, 75);
        public Color contextMenuFontColor { get; set; } = Color.White;

        #endregion
        public MainFormStyle()
        {
            contextMenuOpacity = 0.9f;
            useImersiveDarkMode = true;
            backgroundColor = Color.FromArgb(42, 47, 56);
            lightBackgroundColor = Color.FromArgb(52, 57, 65);
            darkBackgroundColor = Color.FromArgb(28, 32, 38);
            textColor = Color.FromArgb(235, 235, 235);
            borderColor = Color.FromArgb(28, 32, 38);
            menuHighlightColor = Color.FromArgb(30, 34, 40);
            menuHighlightBorderColor = Color.FromArgb(116, 129, 152);
            menuBorderColor = Color.FromArgb(22, 26, 31);
            menuCheckBackgroundColor = Color.FromArgb(26, 64, 75);
            separatorDarkColor = Color.FromArgb(22, 26, 31);
            separatorLightColor = Color.FromArgb(56, 64, 75);
            contextMenuFontColor = Color.White;
        }
    }
    
    public class RegionCaptureStyle
    {
        #region Region Capture Themes
        public Color BackgroundOverlayColor
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
        private Color backgroundOverlayColor = Color.Black;
        public ushort BackgroundOverlayOpacity
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
        private ushort backgroundOverlayOpacity = 36;

        public Color ScreenWideCrosshairColor
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
        private Color screenWideCrosshairColor = Color.FromArgb(249, 0, 187);
        public ushort ScreenWideCrosshairOpacity
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
        private ushort screenWideCrosshairOpacity = 255;

        public Color MagnifierCrosshairColor
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
        private Color magnifierCrosshairColor = Color.LightBlue;
        public ushort MagnifierCrosshairOpacity
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
        private ushort magnifierCrosshairOpacity = 125;

        public Color MagnifierGridColor
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
        private Color magnifierGridColor = Color.Black;
        public ushort MagnifierGridOpacity
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
        private ushort magnifierGridOpacity = 255;

        public Color MagnifierBorderColor
        {
            get
            {
                return magnifierBorderColor;
            }
            set
            {
                magnifierBorderOpacity = value.A;
                magnifierBorderColor = value;
            }
        }
        private Color magnifierBorderColor = Color.White;
        public ushort MagnifierBorderOpacity
        {
            get
            {
                return magnifierBorderOpacity;
            }
            set
            {
                if (value != magnifierBorderOpacity)
                {
                    ushort newVal = (ushort)ColorHelper.ValidColor(value);
                    magnifierBorderOpacity = newVal;
                    magnifierBorderColor = Color.FromArgb(newVal,
                        magnifierBorderColor.R,
                        magnifierBorderColor.G,
                        magnifierBorderColor.B);
                }
            }
        }
        private ushort magnifierBorderOpacity = 255;

        public Color infoTextBackgroundColor { get; set; } = Color.FromArgb(39, 43, 50);
        public Color infoTextBorderColor { get; set; } = Color.Black;
        public Color infoTextTextColor { get; set; } = Color.FromArgb(255, 255, 255);
        #endregion
        public RegionCaptureStyle()
        {
            backgroundOverlayColor = Color.Black;
            backgroundOverlayOpacity = 36;
            screenWideCrosshairColor = Color.FromArgb(249, 0, 187);
            screenWideCrosshairOpacity = 255;
            MagnifierCrosshairColor = Color.LightBlue;
            magnifierCrosshairOpacity = 125;
            magnifierGridColor = Color.Black;
            magnifierGridOpacity = 255;
            magnifierBorderColor = Color.White;
            magnifierBorderOpacity = 255;
            infoTextBackgroundColor = Color.FromArgb(39, 43, 50);
            infoTextBorderColor = Color.Black;
            infoTextTextColor = Color.White;
        }
    }

    public class ClipStyle
    {
        public Color clipBorderColor { get; set; } = Color.FromArgb(249, 0, 187);

        public ClipStyle()
        {
            clipBorderColor = Color.FromArgb(249, 0, 187);
        }
    }

    public class ApplicationStyles
    {
        public static event EventHandler UpdateSylesEvent;
        public static ApplicationStyles currentStyle { get; set; } = new ApplicationStyles();
        public MainFormStyle mainFormStyle { get; set; }
        public RegionCaptureStyle regionCaptureStyle { get; set; }
        public ClipStyle clipStyle { get; set; }

        public ApplicationStyles()
        {
            mainFormStyle = new MainFormStyle();
            regionCaptureStyle = new RegionCaptureStyle();
            clipStyle = new ClipStyle();
        }

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
            e.Item.ForeColor = ApplicationStyles.currentStyle.mainFormStyle.textColor;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (e.Item is ToolStripDropDownButton tsddb && tsddb.Owner is ToolStrip)
            {
                e.Direction = ArrowDirection.Right;
            }
            e.ArrowColor = ApplicationStyles.currentStyle.mainFormStyle.textColor;
            base.OnRenderArrow(e);
        }
    }

    public class CustomColorTable : ProfessionalColorTable
    {
        public override Color ButtonSelectedHighlight
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color ButtonSelectedHighlightBorder
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightBorderColor; }
        }
        public override Color ButtonPressedHighlight
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color ButtonPressedHighlightBorder
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightBorderColor; }
        }
        public override Color ButtonCheckedHighlight
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuCheckBackgroundColor; }
        }
        public override Color ButtonCheckedHighlightBorder
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightBorderColor; }
        }
        public override Color ButtonPressedBorder
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightBorderColor; }
        }
        public override Color ButtonSelectedBorder
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightBorderColor; }
        }
        public override Color ButtonCheckedGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuCheckBackgroundColor; }
        }
        public override Color ButtonCheckedGradientMiddle
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuCheckBackgroundColor; }
        }
        public override Color ButtonCheckedGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuCheckBackgroundColor; }
        }
        public override Color ButtonSelectedGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color ButtonSelectedGradientMiddle
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color ButtonSelectedGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color ButtonPressedGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color ButtonPressedGradientMiddle
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color ButtonPressedGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color CheckBackground
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuCheckBackgroundColor; }
        }
        public override Color CheckSelectedBackground
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuCheckBackgroundColor; }
        }
        public override Color CheckPressedBackground
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuCheckBackgroundColor; }
        }
        public override Color GripDark
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.separatorDarkColor; }
        }
        public override Color GripLight
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.separatorLightColor; }
        }
        public override Color ImageMarginGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ImageMarginGradientMiddle
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ImageMarginGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ImageMarginRevealedGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ImageMarginRevealedGradientMiddle
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ImageMarginRevealedGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color MenuStripGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color MenuStripGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color MenuItemSelected
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color MenuItemBorder
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuBorderColor; }
        }
        public override Color MenuBorder
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuBorderColor; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color MenuItemPressedGradientMiddle
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.menuHighlightColor; }
        }
        public override Color RaftingContainerGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color RaftingContainerGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color SeparatorDark
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.separatorDarkColor; }
        }
        public override Color SeparatorLight
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.separatorLightColor; }
        }
        public override Color StatusStripGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color StatusStripGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ToolStripBorder
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ToolStripGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ToolStripGradientMiddle
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ToolStripGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ToolStripContentPanelGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ToolStripContentPanelGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ToolStripPanelGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color ToolStripPanelGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color OverflowButtonGradientBegin
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color OverflowButtonGradientMiddle
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
        public override Color OverflowButtonGradientEnd
        {
            get { return ApplicationStyles.currentStyle.mainFormStyle.backgroundColor; }
        }
    }
}
