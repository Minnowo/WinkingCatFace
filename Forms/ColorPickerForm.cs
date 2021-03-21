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
    public partial class ColorPickerForm : Form
    {
        private RadioButton checkedButton = null;
        private bool preventBroadCast = false;
        private bool preventUpdate = false;
        private bool isHandleCreated = false;
        public ColorPickerForm()
        {
            InitializeComponent();
            this.Text = "ColorPicker";
            this.MaximizeBox = false;

            foreach(ColorFormat format in Enum.GetValues(typeof(ColorFormat)))
                cbCopyFormat.Items.Add(format);
            cbCopyFormat.SelectedItem = ClipboardHelper.copyFormat;



            ccbRGBColor.ColorFormat = ColorFormat.RGB;
            ccbRGBColor.DecimalPlaces = 0;

            ccbHSBColor.ColorFormat = ColorFormat.HSB;

            ccbHSLColor.ColorFormat = ColorFormat.HSL;

            ccbAdobeRGBColor.ColorFormat = ColorFormat.AdobeRGB;

            ccbYXYColor.ColorFormat = ColorFormat.Yxy;
            ccbYXYColor.DecimalPlaces = 3;

            ccbXYZColor.ColorFormat = ColorFormat.XYZ;
            ccbXYZColor.DecimalPlaces = 2;

            ccbCMYKColor.ColorFormat = ColorFormat.CMYK;

            rbHSBHue.Checked = true;
            checkedButton = rbHSBHue;

            ccbRGBColor.ColorChanged += ComboBoxColorChanged;
            ccbHSBColor.ColorChanged += ComboBoxColorChanged;
            ccbHSLColor.ColorChanged += ComboBoxColorChanged;
            ccbAdobeRGBColor.ColorChanged += ComboBoxColorChanged;
            ccbYXYColor.ColorChanged += ComboBoxColorChanged;
            ccbXYZColor.ColorChanged += ComboBoxColorChanged;
            ccbCMYKColor.ColorChanged += ComboBoxColorChanged;

            colorPicker.ColorChanged += ColorPicker_ColorChanged;

            rbRed.CheckedChanged += RadioButtonChanged;
            rbGreen.CheckedChanged += RadioButtonChanged;
            rbBlue.CheckedChanged += RadioButtonChanged;
            rbHSBHue.CheckedChanged += RadioButtonChanged;
            rbHSBSaturation.CheckedChanged += RadioButtonChanged;
            rbHSBBrightness.CheckedChanged += RadioButtonChanged;
            rbHSLHue.CheckedChanged += RadioButtonChanged;
            rbHSLSaturation.CheckedChanged += RadioButtonChanged;
            rbHSLLightness.CheckedChanged += RadioButtonChanged;
            rbAdobeRGBRed.CheckedChanged += RadioButtonChanged;
            rbAdobeRGBGreen.CheckedChanged += RadioButtonChanged;
            rbAdobeRGBBlue.CheckedChanged += RadioButtonChanged;
            rbX.CheckedChanged += RadioButtonChanged;

            this.HandleCreated += ColorPickerForm_HandleCreated;
        }

        private void ColorPickerForm_HandleCreated(object sender, EventArgs e)
        {
            isHandleCreated = true;
            UpdateTheme();
        }

        private void ComboBoxColorChanged(object sender, ColorEventArgs e)
        {
            preventUpdate = true;
            colorPicker.SelectedColor = e.Color;
            ccbRGBColor.UpdateColor(e.Color);
            ccbHSBColor.UpdateColor(e.Color);
            ccbHSLColor.UpdateColor(e.Color);
            ccbAdobeRGBColor.UpdateColor(e.Color);
            ccbYXYColor.UpdateColor(e.Color);
            ccbXYZColor.UpdateColor(e.Color);
            ccbCMYKColor.UpdateColor(e.Color);

            displayColorLabel.BackColor = e.Color;
            preventUpdate = false;
        }

        private void ChangeDrawStyle(string checkboxName)
        {
            switch (checkboxName)
            {
                case "rbAdobeRGBRed":
                case "rbRed":
                    colorPicker.DrawStyle = DrawStyles.Red;
                    break;
                case "rbAdobeRGBGreen":
                case "rbGreen":
                    colorPicker.DrawStyle = DrawStyles.Green;
                    break;
                case "rbAdobeRGBBlue":
                case "rbBlue":
                    colorPicker.DrawStyle = DrawStyles.Blue;
                    break;
                case "rbHSBHue":
                    colorPicker.DrawStyle = DrawStyles.HSBHue;
                    break;
                case "rbHSBSaturation":
                    colorPicker.DrawStyle = DrawStyles.HSBSaturation;
                    break;
                case "rbHSBBrightness":
                    colorPicker.DrawStyle = DrawStyles.HSBBrightness;
                    break;
                case "rbHSLHue":
                    colorPicker.DrawStyle = DrawStyles.HSLHue;
                    break;
                case "rbHSLSaturation":
                    colorPicker.DrawStyle = DrawStyles.HSLSaturation;
                    break;
                case "rbHSLLightness":
                    colorPicker.DrawStyle = DrawStyles.HSLLightness;
                    break;
                case "rbX":
                    colorPicker.DrawStyle = DrawStyles.xyz;
                    break;
            }
        }

        private void RadioButtonChanged(object sender, EventArgs e)
        {
            if (preventBroadCast)
                return;
            if(checkedButton != null)
            {
                preventBroadCast = true;
                checkedButton.Checked = false;
                checkedButton = (RadioButton)sender;
                checkedButton.Checked = true;
                ChangeDrawStyle(checkedButton.Name);
            }
            else
            {
                checkedButton = (RadioButton)sender;
            }
            preventBroadCast = false;
        }

        private void ColorPicker_ColorChanged(object sender, ColorEventArgs e)
        {
            if (preventUpdate)
                return;
            if (colorPicker.DrawStyle != DrawStyles.xyz)
            {
                ccbRGBColor.UpdateColor(colorPicker.SelectedColor);
                ccbHSBColor.UpdateColor(colorPicker.SelectedColor);
                ccbHSLColor.UpdateColor(colorPicker.SelectedColor);
                ccbAdobeRGBColor.UpdateColor(colorPicker.SelectedColor);
                ccbYXYColor.UpdateColor(colorPicker.SelectedColor);
                ccbXYZColor.UpdateColor(colorPicker.SelectedColor);
                ccbCMYKColor.UpdateColor(colorPicker.SelectedColor);
                textBox1.Text = colorPicker.SelectedColor.ToHex();
                displayColorLabel.BackColor = colorPicker.SelectedColor;
            }
            else
            {
                ccbRGBColor.UpdateColor(colorPicker.AbsoluteColor);
                ccbHSBColor.UpdateColor(colorPicker.AbsoluteColor);
                ccbHSLColor.UpdateColor(colorPicker.AbsoluteColor);
                ccbAdobeRGBColor.UpdateColor(colorPicker.AbsoluteColor);
                ccbYXYColor.UpdateColor(colorPicker.AbsoluteColor);
                ccbXYZColor.UpdateColor(colorPicker.AbsoluteColor);
                ccbCMYKColor.UpdateColor(colorPicker.AbsoluteColor);
                textBox1.Text = colorPicker.AbsoluteColor.ToHex();
                displayColorLabel.BackColor = colorPicker.AbsoluteColor;
            }
            
        }

        public void UpdateTheme()
        {
            if (ApplicationStyles.currentStyle.mainFormStyle.useImersiveDarkMode && isHandleCreated)
            {
                NativeMethods.UseImmersiveDarkMode(Handle, true);
                this.Icon = Properties.Resources._3white;
            }
            else
            {
                NativeMethods.UseImmersiveDarkMode(Handle, false);
                this.Icon = Properties.Resources._3black;
            }
            ApplicationStyles.ApplyCustomThemeToControl(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (colorPicker.DrawStyle != DrawStyles.xyz)
            {
                ClipboardHelper.FormatCopyColor((ColorFormat)cbCopyFormat.SelectedItem, colorPicker.SelectedColor);
            }
            else
            {
                ClipboardHelper.FormatCopyColor((ColorFormat)cbCopyFormat.SelectedItem, colorPicker.AbsoluteColor);
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Color a;
            ColorHelper.ParseHex(textBox1.Text, out a);
            if (a != Color.Empty)
                colorPicker.SelectedColor = a;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //colorPicker.SelectedColor = ColorHelper.HexToColor(textBox2.Text);
        }
    }
}
