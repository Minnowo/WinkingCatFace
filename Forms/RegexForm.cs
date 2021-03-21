﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using WinkingCat.HelperLibs;

namespace WinkingCat
{
    public partial class RegexForm : Form
    {
        private Regex reg;
        private bool isHandleCreated = false;
        public RegexForm()
        {
            InitializeComponent();
            HandleCreated += RegexForm_HandleCreated;
            ApplicationStyles.UpdateStylesEvent += ApplicationStyles_UpdateStylesEvent;
        }

        private void RegexForm_HandleCreated(object sender, EventArgs e)
        {
            isHandleCreated = true;
            UpdateTheme();
        }

        private void ApplicationStyles_UpdateStylesEvent(object sender, EventArgs e)
        {
            UpdateTheme();
        }

        public void UpdateTheme()
        {
            if (ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode && isHandleCreated)
            {
                NativeMethods.UseImmersiveDarkMode(Handle, true);
                this.Icon = ApplicationStyles.whiteIcon; //Properties.Resources._3white;
            }
            else
            {
                NativeMethods.UseImmersiveDarkMode(Handle, false);
                this.Icon = ApplicationStyles.blackIcon; //Properties.Resources._3black;
            }
            ApplicationStyles.ApplyCustomThemeToControl(this);
        }

        private void btnRunCheck_Click(object sender, EventArgs e)
        {
            try
            {
                reg = new Regex(textBox1.Text);

                lRegexMatch.Text = reg.IsMatch(textBox2.Text).ToString();
                tbException.Text = "";
            }
            catch(Exception ex)
            {
                lRegexMatch.Text = "Null";
                tbException.Text = ex.ToString();
            }
        }
    }
}