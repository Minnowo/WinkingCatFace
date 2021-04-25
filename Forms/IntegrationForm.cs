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
    public partial class IntegrationForm : Form
    {
        private bool isHandleCreated = false;
        public IntegrationForm()
        {
            InitializeComponent();
            this.HandleCreated += HandleCreated_Event;

        }
        public void UpdateTheme()
        {
            try
            {
                this.BackColor = ApplicationStyles.currentStyle.mainFormStyle.backgroundColor;
                ApplicationStyles.ApplyCustomThemeToControl(this);
                Refresh();
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
            }
        }

        public void HandleCreated_Event(object sender, EventArgs e)
        {
            isHandleCreated = true;
            UpdateTheme();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Helpers.LaunchProcess("", "", true);
        }


    }
}
