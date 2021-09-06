using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    [DefaultEvent("ColorChanged")]
    public partial class ColorComboBox : UserControl
    {
        public event ColorEventHandler ColorChanged;

        public COLOR CurrentColor
        {
            get
            {
                return GetColor();
            }
        }

        public decimal[] Values
        {
            get
            {
                return values;
            }
            set
            {
                if (value.Length == values.Length)
                {
                    values = value;
                    UpdateValues();
                }
            }
        }
        public decimal[] MinValues
        {
            get
            {
                return minValues;
            }
            set
            {
                if (value.Length == values.Length)
                {
                    minValues = value;
                    UpdateMin();
                }
            }
        }
        public decimal[] MaxValues
        {
            get
            {
                return maxValues;
            }
            set
            {
                if (value.Length == values.Length)
                {
                    maxValues = value;
                    UpdateMax();
                }
            }
        }

        public ColorFormat ColorFormat
        {
            get
            {
                return colorFormat;
            }
            set
            {
                if (value != colorFormat)
                {
                    colorFormat = value;
                    UpdateColorFormat();
                }
            }
        }

        public byte DecimalPlaces
        {
            get
            {
                return decimalPlaces;
            }
            set
            {
                this.decimalPlaces = value;
                UpdateDecimalPlaces();
            }
        }

        private decimal[] minValues;
        private decimal[] maxValues;
        private decimal[] values;

        private byte decimalPlaces = 1;
        private bool preventOverflow = false;

        [DefaultValue(true)]
        private ColorFormat colorFormat = ColorFormat.RGB;
        public ColorComboBox()
        {
            InitializeComponent();
            UpdateColorFormat();
            CreateNumericUpDown();
            UpdateMin();
            UpdateMax();
            UpdateValues();
        }

        private COLOR GetColor()
        {
            COLOR mycolor = Color.White;
            switch (this.colorFormat)
            {
                case ColorFormat.RGB:
                    mycolor.ARGB = COLOR.FromARGB((short)values[0], (short)values[1], (short)values[2]);
                    mycolor.UpdateARGB();
                    break;

                case ColorFormat.ARGB:
                    mycolor.ARGB = COLOR.FromARGB((short)values[0], (short)values[1], (short)values[2], (short)values[3]);
                    mycolor.UpdateARGB();
                    break;

                case ColorFormat.CMYK:
                    mycolor.CMYK = CMYK.FromCMYK100((float)values[0], (float)values[1], (float)values[2], (float)values[3]);
                    mycolor.UpdateCMYK();
                    break;

                case ColorFormat.HSB:
                case ColorFormat.HSV:
                    mycolor.HSB = HSB.FromHSB360((float)values[0], (float)values[1], (float)values[2]);
                    mycolor.UpdateHSB();
                    break;

                case ColorFormat.HSL:
                    mycolor.HSL = HSL.FromHSL360((float)values[0], (float)values[1], (float)values[2]);
                    mycolor.UpdateHSL();
                    break;
            }

            return mycolor;
        }

        public void OnColorChanged()
        {
            OnColorChanged(GetColor());
        }
        public void OnColorChanged(COLOR color)
        {
            if (ColorChanged != null)
            {
                ColorChanged(this, new ColorEventArgs(color, this.colorFormat));
            }
        }

        public void UpdateColor(COLOR newColor)
        {
            if (preventOverflow)
                return;

            preventOverflow = true;

            switch (ColorFormat)
            {
                case ColorFormat.RGB:
                    values[0] = newColor.ARGB.R;
                    values[1] = newColor.ARGB.G;
                    values[2] = newColor.ARGB.B;
                    break;
                case ColorFormat.ARGB:
                    values[0] = newColor.ARGB.A;
                    values[1] = newColor.ARGB.R;
                    values[2] = newColor.ARGB.G;
                    values[3] = newColor.ARGB.B;
                    break;
                case ColorFormat.CMYK:
                    values[0] = (decimal)newColor.CMYK.C100;
                    values[1] = (decimal)newColor.CMYK.M100;
                    values[2] = (decimal)newColor.CMYK.Y100;
                    values[3] = (decimal)newColor.CMYK.K100;
                    break;
                case ColorFormat.HSB:
                case ColorFormat.HSV:
                    values[0] = (decimal)newColor.HSB.Hue360;
                    values[1] = (decimal)newColor.HSB.Saturation100;
                    values[2] = (decimal)newColor.HSB.Brightness100;
                    break;
                case ColorFormat.HSL:
                    values[0] = (decimal)newColor.HSL.Hue360;
                    values[1] = (decimal)newColor.HSL.Saturation100;
                    values[2] = (decimal)newColor.HSL.Lightness100;
                    break;
            }
            UpdateValues();

            preventOverflow = false;
        }

        public void UpdateMin()
        {
            if (this.minValues == null)
                return;

            for (int index = 0; index < minValues.Length; index++)
            {
                ((NumericUpDown)this.Controls[index]).Minimum = minValues[index];
            }

        }

        public void UpdateMax()
        {
            if (this.minValues == null)
                return;

            for (int index = 0; index < maxValues.Length; index++)
            {
                ((NumericUpDown)this.Controls[index]).Maximum = maxValues[index];
            }
        }

        public void UpdateValues()
        {
            if (this.values == null)
                return;

            for (int index = 0; index < values.Length; index++)
            {
                ((NumericUpDown)this.Controls[index]).Value = values[index].Clamp(MinValues[index], MaxValues[index]);
            }
        }

        public void UpdateDecimalPlaces()
        {
            foreach (NumericUpDown control in this.Controls.OfType<NumericUpDown>())
            {
                control.DecimalPlaces = decimalPlaces;
            }
        }

        public void UpdateColorFormat()
        {
            switch (colorFormat)
            {
                case ColorFormat.RGB:
                    ResizeValues(3);
                    values = new decimal[] { 1M, 1M, 1M };
                    minValues = new decimal[] { 0, 0, 0 };
                    maxValues = new decimal[] { 255M, 255M, 255M };
                    ColorComboBox_ClientSizeChanged(null, EventArgs.Empty);
                    break;
                case ColorFormat.ARGB:
                    ResizeValues(4);
                    values = new decimal[] { 1M, 1M, 1M, 1M };
                    minValues = new decimal[] { 0, 0, 0, 0 };
                    maxValues = new decimal[] { 255M, 255M, 255M, 255M };
                    ColorComboBox_ClientSizeChanged(null, EventArgs.Empty);
                    break;
                case ColorFormat.HSB:
                case ColorFormat.HSV:
                    ResizeValues(3);
                    values = new decimal[] { 1M, 1M, 1M };
                    minValues = new decimal[] { 0, 0, 0 };
                    maxValues = new decimal[] { 360M, 100M, 100M };
                    ColorComboBox_ClientSizeChanged(null, EventArgs.Empty);
                    break;
                case ColorFormat.HSL:
                    ResizeValues(3);
                    values = new decimal[] { 1M, 1M, 1M };
                    minValues = new decimal[] { 0, 0, 0 };
                    maxValues = new decimal[] { 360M, 100M, 100M };
                    ColorComboBox_ClientSizeChanged(null, EventArgs.Empty);
                    break;
                case ColorFormat.CMYK:
                    ResizeValues(4);
                    values = new decimal[] { 1M, 1M, 1M, 1M };
                    minValues = new decimal[] { 0M, 0M, 0M, 0M };
                    maxValues = new decimal[] { 100M, 100M, 100M, 100M };
                    ColorComboBox_ClientSizeChanged(null, EventArgs.Empty);
                    break;
            }
            UpdateMin();
            UpdateMax();
            UpdateValues();
        }


        private void ColorComboBox_ClientSizeChanged(object sender, EventArgs e)
        {
            COLOR c = GetColor();
            CreateNumericUpDown();
            UpdateColor(c);
        }

        private void ResizeValues(int newSize)
        {
            Array.Resize(ref values, newSize);
            Array.Resize(ref minValues, newSize);
            Array.Resize(ref maxValues, newSize);
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (preventOverflow)
                return;

            preventOverflow = true;

            values[((NumericUpDown)sender).TabIndex] = ((NumericUpDown)sender).Value;
            OnColorChanged();

            preventOverflow = false;
        }

        private void CreateNumericUpDown()
        {
            this.Controls.Clear();

            if (values == null | minValues == null | maxValues == null)
                return;

            int controlWidth = this.Size.Width / values.Length;

            for (int i = 0; i < values.Length; i++)
            {
                NumericUpDown n = new NumericUpDown() { Text = "", Width = controlWidth, Height = this.Height };
                n.Margin = new Padding(0);
                n.Location = new Point(i * controlWidth, 0);
                n.AutoSize = true;
                n.DecimalPlaces = this.decimalPlaces;
                n.Minimum = minValues[i];
                n.Maximum = maxValues[i];
                n.Value = values[i];
                n.ValueChanged += NumericUpDown_ValueChanged;
                this.Controls.Add(n);
            }
            Refresh();
        }
    }
}
