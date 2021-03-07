using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinkingCat.HelperLibs;

namespace WinkingCat
{
    public partial class MainWindowStylesForm : Form
    {
        public MainWindowStylesForm()
        {
            InitializeComponent();
            propertyGrid1.PropertySort = PropertySort.NoSort;
            propertyGrid1.SelectedObject = ApplicationStyles.currentStyle.mainFormStyle;
            propertyGrid1.PropertyValueChanged += PropertyGrid1_PropertyValueChanged;
            UpdateTheme();
        }
        public void UpdateTheme()
        {
            ApplicationStyles.ApplyCustomThemeToControl(this);
        }
        private void PropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ApplicationStyles.UpdateAll();
            Invalidate();
        }
    }
}
