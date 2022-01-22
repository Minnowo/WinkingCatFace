using System;
using System.Drawing;
using System.Windows.Forms;


namespace WinkingCat.HelperLibs
{
    public partial class ColorPickerForm : BaseForm
    {
        public ColorSpaceDrawStyle ColorBoxDrawStyle
        {
            get
            {
                return cp_ColorPickerMain.DrawStyle;
            }
            set
            {
                cp_ColorPickerMain.DrawStyle = value;
            }
        }

        public COLOR SelectedColor
        {
            get { return cp_ColorPickerMain.SelectedColor; }
        }

        private bool preventOverflow = false;
        private RadioButton currentRad = null;

        private const string RB_DISPLAY_RED = "rb_DisplayRed";
        private const string RB_DISPLAY_GREEN = "rb_DisplayGreen";
        private const string RB_DISPLAY_BLUE = "rb_DisplayBlue";

        private const string RB_DISPLAY_HSB_HUE= "rb_DisplayHSBHue";
        private const string RB_DISPLAY_HSB_SAT = "rb_DisplayHSBSaturation";
        private const string RB_DISPLAY_HSB_BRI = "rb_DisplayBrightness";

        private const string RB_DISPLAY_HSL_HUE = "rb_DisplayHSLHue";
        private const string RB_DISPLAY_HSL_SAT = "rb_DisplayHSLSaturation";
        private const string RB_DISPLAY_HSL_LIG = "rb_DisplayLightness";

        public ColorPickerForm(COLOR startingColor)
        {
            InitializeComponent();

            this.Text = "ColorPicker";
            this.MaximizeBox = false;
            this.KeyPreview = true;

            nudAlphaValue.Value = 255;

            rb_DisplayHSBHue.Checked = true;

            UpdateColors(startingColor);
            base.RegisterEvents();
        }

        public ColorPickerForm() : this(Color.Red)
        {
            
        }


        private void ColorPicker_ColorChanged(object sender, ColorEventArgs e)
        {
            if (preventOverflow)
                return;

            e.Color.A = (byte)nudAlphaValue.Value;
            UpdateColors(e.Color);
        }

        private void ColorComboBox_ColorChanged(object sender, ColorEventArgs e)
        {
            e.Color.A = (byte)nudAlphaValue.Value;
            UpdateColors(e.Color);
        }

        public COLOR GetCurrentColor()
        {
            return cp_ColorPickerMain.SelectedColor;
        }

        public void UpdateColors(COLOR e)
        {
            preventOverflow = true;

            cp_ColorPickerMain.SelectedColor = e;

            nudAlphaValue.Value = e.A;

            ccb_RGB.UpdateColor(e);
            ccb_HSB.UpdateColor(e);
            ccb_HSL.UpdateColor(e);
            ccb_CMYK.UpdateColor(e);

            if (e.isTransparent)
            {
                tb_HexDisplay.Text = ColorHelper.ColorToHex(e, ColorFormat.ARGB);
                tb_DecimalDisplay.Text = ColorHelper.ColorToDecimal(e, ColorFormat.ARGB).ToString();
            }
            else
            {
                tb_HexDisplay.Text = ColorHelper.ColorToHex(e, ColorFormat.RGB);
                tb_DecimalDisplay.Text = ColorHelper.ColorToDecimal(e, ColorFormat.RGB).ToString();
            }

            cd_ColorDisplayMain.CurrentColor = e;

            preventOverflow = false;
        }

        public void UpdateColors(Color e)
        {
            UpdateColors(new COLOR(e));
        }

        private void PasteColor_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null)
                return;

            string clipboardText = Clipboard.GetText();

            if (string.IsNullOrEmpty(clipboardText))
                return;

            switch (b.Name)
            {
                case "btn_PasteRGB":

                    Color rgb = Color.White;

                    if(ColorHelper.ParseRGB(clipboardText, out rgb))
                    {
                        cp_ColorPickerMain.SelectedColor = rgb;
                    }
                    break;

                case "btn_PasteHSB":

                    HSB hsb = Color.White;

                    if (ColorHelper.ParseHSB(clipboardText, out hsb))
                    {
                        cp_ColorPickerMain.SelectedColor = hsb.ToColor();
                    }
                    break;
                case "btn_PasteHSL":

                    HSL hsl = Color.White;

                    if (ColorHelper.ParseHSL(clipboardText, out hsl))
                    {
                        cp_ColorPickerMain.SelectedColor = hsl.ToColor();
                    }
                    break;
                case "btn_PasteCMYK":

                    CMYK cmyk = Color.White;

                    if (ColorHelper.ParseCMYK(clipboardText, out cmyk))
                    {
                        cp_ColorPickerMain.SelectedColor = cmyk.ToColor();
                    }
                    break;
            }
        }

        private void CopyColor_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b == null)
                return;

            switch (b.Name)
            {
                case "btn_CopyRGB":
                    ClipboardHelper.FormatCopyColor(ColorFormat.RGB, cp_ColorPickerMain.SelectedColor);
                    break;
                case "btn_CopyHSB":
                    ClipboardHelper.FormatCopyColor(ColorFormat.HSB, cp_ColorPickerMain.SelectedColor);
                    break;
                case "btn_CopyHSL":
                    ClipboardHelper.FormatCopyColor(ColorFormat.HSL, cp_ColorPickerMain.SelectedColor);
                    break;
                case "btn_CopyCMYK":
                    ClipboardHelper.FormatCopyColor(ColorFormat.CMYK, cp_ColorPickerMain.SelectedColor);
                    break;
            }
        }

        private void CloseForm_Event(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void HexValue_Changed(object sender, EventArgs e)
        {
            if (preventOverflow)
                return;

            preventOverflow = true;
            try
            {
                UpdateColors(ColorHelper.HexToColor(tb_HexInput.Text));   
            }
            catch
            {

            }
            preventOverflow = false;
        }

        private void DecimalValue_Changed(object sender, EventArgs e)
        {
            if (preventOverflow)
                return;

            preventOverflow = true;
            try
            {
                if (int.TryParse(tb_DecimalInput.Text, out int dec))
                {
                    if (dec.ToString().Length > 8)
                    {
                        Color c = ColorHelper.DecimalToColor(dec, ColorFormat.ARGB);
                        UpdateColors(c);
                    }
                    else
                    {
                        UpdateColors(ColorHelper.DecimalToColor(dec));
                    }
                }
            }
            catch
            {

            }
            preventOverflow = false;
        }

        private void Alpha_ValueChanged(object sender, EventArgs e)
        {
            if (preventOverflow)
                return;

            preventOverflow = true;

            UpdateColors(
                new COLOR(
                    (byte)nudAlphaValue.Value,
                    cd_ColorDisplayMain.CurrentColor.R, 
                    cd_ColorDisplayMain.CurrentColor.G, 
                    cd_ColorDisplayMain.CurrentColor.B
                    ));

            preventOverflow = false;
        }

        private void RadioButton_CheckChanged(object sender, EventArgs e)
        {
            if (preventOverflow)
                return;

            preventOverflow = true;

            if (currentRad != null)
                currentRad.Checked = false;

            currentRad = sender as RadioButton;

            switch (currentRad.Name)
            {
                case RB_DISPLAY_RED:
                    ColorBoxDrawStyle = ColorSpaceDrawStyle.Red;
                    break;
                case RB_DISPLAY_GREEN:
                    ColorBoxDrawStyle = ColorSpaceDrawStyle.Green;
                    break;
                case RB_DISPLAY_BLUE:
                    ColorBoxDrawStyle = ColorSpaceDrawStyle.Blue;
                    break;

                case RB_DISPLAY_HSB_HUE:
                    ColorBoxDrawStyle = ColorSpaceDrawStyle.HSBHue;
                    break;
                case RB_DISPLAY_HSB_SAT:
                    ColorBoxDrawStyle = ColorSpaceDrawStyle.HSBSaturation;
                    break;
                case RB_DISPLAY_HSB_BRI:
                    ColorBoxDrawStyle = ColorSpaceDrawStyle.HSBBrightness;
                    break;

                case RB_DISPLAY_HSL_HUE:
                    ColorBoxDrawStyle = ColorSpaceDrawStyle.HSLHue;
                    break;
                case RB_DISPLAY_HSL_SAT:
                    ColorBoxDrawStyle = ColorSpaceDrawStyle.HSLSaturation;
                    break;
                case RB_DISPLAY_HSL_LIG:
                    ColorBoxDrawStyle = ColorSpaceDrawStyle.HSLLightness;
                    break;
            }

            preventOverflow = false;
        }

        private void ScreenColorPicker_Click(object sender, EventArgs e)
        {
            if(RegionCaptureHelper.GetRegionResultColor(out COLOR c))
            {
                UpdateColors(c);
            }
        }
    }
}
