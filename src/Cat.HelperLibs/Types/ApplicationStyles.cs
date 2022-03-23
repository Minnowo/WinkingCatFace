using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{


    public class ApplicationStyles
    {
        public delegate void ThemeUpdated();
        public static event ThemeUpdated UpdateThemeEvent;
        public static ApplicationStyles currentStyle { get; set; } = new ApplicationStyles();

        public static class Resources
        {
            public static Image Folder16
            {
                get
                {
                    return Properties.Resources.NewFolder16;
                }
            }
        }

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

        public static void ApplyCustomThemeToControl(Control control)
        {
            switch (control)
            {
                case Button btn:
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = SettingsManager.MainFormSettings.borderColor;
                    btn.ForeColor = SettingsManager.MainFormSettings.textColor;
                    btn.BackColor = SettingsManager.MainFormSettings.lightBackgroundColor;
                    return;
                case CheckBox cb when cb.Appearance == Appearance.Button:
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.FlatAppearance.BorderColor = SettingsManager.MainFormSettings.borderColor;
                    cb.ForeColor = SettingsManager.MainFormSettings.textColor;
                    cb.BackColor = SettingsManager.MainFormSettings.lightBackgroundColor;
                    return;
                case TextBox tb:
                    tb.ForeColor = SettingsManager.MainFormSettings.textColor;
                    tb.BackColor = SettingsManager.MainFormSettings.lightBackgroundColor;
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    return;
                case ComboBox cb:
                    cb.FlatStyle = FlatStyle.Flat;
                    cb.ForeColor = SettingsManager.MainFormSettings.textColor;
                    cb.BackColor = SettingsManager.MainFormSettings.lightBackgroundColor;
                    return;
                case ListBox lb:
                    lb.ForeColor = SettingsManager.MainFormSettings.textColor;
                    lb.BackColor = SettingsManager.MainFormSettings.lightBackgroundColor;
                    return;
                case ListView lv:
                    lv.ForeColor = SettingsManager.MainFormSettings.textColor;
                    lv.BackColor = SettingsManager.MainFormSettings.lightBackgroundColor;
                    lv.SupportCustomTheme();
                    return;
                case SplitContainer sc:
                    sc.Panel1.BackColor = SettingsManager.MainFormSettings.backgroundColor;
                    sc.Panel2.BackColor = SettingsManager.MainFormSettings.backgroundColor;
                    break;
                case PropertyGrid pg:
                    pg.CategoryForeColor = SettingsManager.MainFormSettings.textColor;
                    pg.CategorySplitterColor = SettingsManager.MainFormSettings.backgroundColor;
                    pg.LineColor = SettingsManager.MainFormSettings.backgroundColor;
                    pg.SelectedItemWithFocusForeColor = SettingsManager.MainFormSettings.backgroundColor;
                    pg.SelectedItemWithFocusBackColor = SettingsManager.MainFormSettings.textColor;
                    pg.ViewForeColor = SettingsManager.MainFormSettings.textColor;
                    pg.ViewBackColor = SettingsManager.MainFormSettings.lightBackgroundColor;
                    pg.ViewBorderColor = SettingsManager.MainFormSettings.borderColor;
                    pg.HelpForeColor = SettingsManager.MainFormSettings.textColor;
                    pg.HelpBackColor = SettingsManager.MainFormSettings.backgroundColor;
                    pg.HelpBorderColor = SettingsManager.MainFormSettings.borderColor;
                    return;
                case DataGridView dgv:
                    dgv.BackgroundColor = SettingsManager.MainFormSettings.lightBackgroundColor;
                    dgv.GridColor = SettingsManager.MainFormSettings.borderColor;
                    dgv.DefaultCellStyle.BackColor = SettingsManager.MainFormSettings.lightBackgroundColor;
                    dgv.DefaultCellStyle.SelectionBackColor = SettingsManager.MainFormSettings.lightBackgroundColor;
                    dgv.DefaultCellStyle.ForeColor = SettingsManager.MainFormSettings.textColor;
                    dgv.DefaultCellStyle.SelectionForeColor = SettingsManager.MainFormSettings.textColor;
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = SettingsManager.MainFormSettings.backgroundColor;
                    dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = SettingsManager.MainFormSettings.backgroundColor;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = SettingsManager.MainFormSettings.textColor;
                    dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = SettingsManager.MainFormSettings.textColor;
                    dgv.EnableHeadersVisualStyles = false;
                    break;
                case LinkLabel ll:
                    ll.LinkColor = SettingsManager.MainFormSettings.linkColor;
                    break;
            }

            control.ForeColor = SettingsManager.MainFormSettings.textColor;
            control.BackColor = SettingsManager.MainFormSettings.backgroundColor;

            
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
            if (UpdateThemeEvent != null)
            {
                UpdateThemeEvent.Invoke();
            }
        }
    }

    

    
}
