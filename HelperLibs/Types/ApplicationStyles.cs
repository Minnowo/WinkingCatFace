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
        public Color imageViewerBackColor { get; set; } = Color.Black;
        public Color linkColor { get; set; } = Color.FromArgb(164, 98, 160);

        #endregion
        public MainFormStyle()
        {
            contextMenuOpacity = 0.9f;
            useImersiveDarkMode = true;
            backgroundColor = Color.FromArgb(40, 42, 54);
            lightBackgroundColor = Color.FromArgb(68, 71, 90);
            darkBackgroundColor = Color.FromArgb(36, 38, 48);
            textColor = Color.FromArgb(248, 248, 242);
            borderColor = Color.FromArgb(33, 35, 43);
            menuHighlightColor = Color.FromArgb(36, 38, 48);
            menuHighlightBorderColor = Color.FromArgb(255, 121, 198);
            menuBorderColor = Color.FromArgb(33, 35, 43);
            menuCheckBackgroundColor = Color.FromArgb(45, 47, 61);
            separatorDarkColor = Color.FromArgb(45, 47, 61);
            separatorLightColor = Color.FromArgb(33, 35, 43);
            contextMenuFontColor = Color.FromArgb(248, 248, 242);
            imageViewerBackColor = Color.Black;
            linkColor = Color.FromArgb(164, 98, 160);
        }
    }
   

    public class ApplicationStyles
    {
        public static event EventHandler UpdateStylesEvent;
        public static ApplicationStyles currentStyle { get; set; } = new ApplicationStyles();
        public MainFormStyle mainFormStyle { get; set; }


        public static Icon blackIcon 
        { 
            get
            {
                return Properties.Resources._3black;
            }
        } 

        public static Icon whiteIcon
        {
            get
            {
                return Properties.Resources._3white;
            }
        }

        public ApplicationStyles()
        {
            mainFormStyle = new MainFormStyle();
        }

        public static void ApplyCustomThemeToControl(Control control)
        {
            switch (control)
            {
                case Button btn:
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = currentStyle.mainFormStyle.borderColor;
                    btn.ForeColor = currentStyle.mainFormStyle.textColor;
                    btn.BackColor = currentStyle.mainFormStyle.lightBackgroundColor;
                    return;
                case CheckBox cb when cb.Appearance == Appearance.Button:
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.FlatAppearance.BorderColor = currentStyle.mainFormStyle.borderColor;
                    cb.ForeColor = currentStyle.mainFormStyle.textColor;
                    cb.BackColor = currentStyle.mainFormStyle.lightBackgroundColor;
                    return;
                case TextBox tb:
                    tb.ForeColor = currentStyle.mainFormStyle.textColor;
                    tb.BackColor = currentStyle.mainFormStyle.lightBackgroundColor;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    return;
                case ComboBox cb:
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.ForeColor = currentStyle.mainFormStyle.textColor;
                    cb.BackColor = currentStyle.mainFormStyle.lightBackgroundColor;
                    return;
                case ListBox lb:
                    lb.ForeColor = currentStyle.mainFormStyle.textColor;
                    lb.BackColor = currentStyle.mainFormStyle.lightBackgroundColor;
                    return;
                case ListView lv:
                    lv.ForeColor = currentStyle.mainFormStyle.textColor;
                    lv.BackColor = currentStyle.mainFormStyle.lightBackgroundColor;
                    lv.SupportCustomTheme();
                    return;
                case SplitContainer sc:
                    sc.Panel1.BackColor = currentStyle.mainFormStyle.backgroundColor;
                    sc.Panel2.BackColor = currentStyle.mainFormStyle.backgroundColor;
                    break;
                case PropertyGrid pg:
                    pg.CategoryForeColor = currentStyle.mainFormStyle.textColor;
                    pg.CategorySplitterColor = currentStyle.mainFormStyle.backgroundColor;
                    pg.LineColor = currentStyle.mainFormStyle.backgroundColor;
                    pg.SelectedItemWithFocusForeColor = currentStyle.mainFormStyle.backgroundColor;
                    pg.SelectedItemWithFocusBackColor = currentStyle.mainFormStyle.textColor;
                    pg.ViewForeColor = currentStyle.mainFormStyle.textColor;
                    pg.ViewBackColor = currentStyle.mainFormStyle.lightBackgroundColor;
                    pg.ViewBorderColor = currentStyle.mainFormStyle.borderColor;
                    pg.HelpForeColor = currentStyle.mainFormStyle.textColor;
                    pg.HelpBackColor = currentStyle.mainFormStyle.backgroundColor;
                    pg.HelpBorderColor = currentStyle.mainFormStyle.borderColor;
                    return;
                case DataGridView dgv:
                    dgv.BackgroundColor = currentStyle.mainFormStyle.lightBackgroundColor;
                    dgv.GridColor = currentStyle.mainFormStyle.borderColor;
                    dgv.DefaultCellStyle.BackColor = currentStyle.mainFormStyle.lightBackgroundColor;
                    dgv.DefaultCellStyle.SelectionBackColor = currentStyle.mainFormStyle.lightBackgroundColor;
                    dgv.DefaultCellStyle.ForeColor = currentStyle.mainFormStyle.textColor;
                    dgv.DefaultCellStyle.SelectionForeColor = currentStyle.mainFormStyle.textColor;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = currentStyle.mainFormStyle.backgroundColor;
                    dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = currentStyle.mainFormStyle.backgroundColor;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = currentStyle.mainFormStyle.textColor;
                    dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = currentStyle.mainFormStyle.textColor;
                    dgv.EnableHeadersVisualStyles = false;
                    break;
                case LinkLabel ll:
                    ll.LinkColor = currentStyle.mainFormStyle.linkColor;
                    break;
            }

            control.ForeColor = currentStyle.mainFormStyle.textColor;
            control.BackColor = currentStyle.mainFormStyle.backgroundColor;

            
            foreach (Control child in control.Controls)
            {
                ApplyCustomThemeToControl(child);
            }
        }

        public static void UpdateAll()
        {
            OnUpdateEvent();
        }

        private static void OnUpdateEvent()
        {
            if (UpdateStylesEvent != null)
            {
                UpdateStylesEvent(null, EventArgs.Empty);
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
