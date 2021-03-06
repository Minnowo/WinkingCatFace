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
    public partial class ColorComboBox : UserControl
    {
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

        private Timer buttonHeldTimer;

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

        public void UpdateColor(_Color newColor)
        {
            switch (ColorFormat)
            {
                case ColorFormat.RGB:
                    values[0] = newColor.argb.R;
                    values[1] = newColor.argb.G;
                    values[2] = newColor.argb.B;
                    break;
                case ColorFormat.ARGB:
                    values[0] = newColor.argb.A;
                    values[1] = newColor.argb.R;
                    values[2] = newColor.argb.G;
                    values[3] = newColor.argb.B;
                    break;
                case ColorFormat.CMYK:
                    values[0] = (decimal)newColor.cmyk.C100;
                    values[1] = (decimal)newColor.cmyk.M100;
                    values[2] = (decimal)newColor.cmyk.Y100;
                    values[3] = (decimal)newColor.cmyk.K100;
                    break;
                case ColorFormat.HSB:
                case ColorFormat.HSV:
                    values[0] = (decimal)newColor.hsb.Hue360;
                    values[1] = (decimal)newColor.hsb.Saturation100;
                    values[2] = (decimal)newColor.hsb.Brightness100;
                    break;
                case ColorFormat.HSL:
                    values[0] = (decimal)newColor.hsl.Hue360;
                    values[1] = (decimal)newColor.hsl.Saturation100;
                    values[2] = (decimal)newColor.hsl.Lightness100;
                    break;
                case ColorFormat.XYZ:
                    values[0] = (decimal)newColor.xyz.X;
                    values[1] = (decimal)newColor.xyz.Y;
                    values[2] = (decimal)newColor.xyz.Z;
                    break;
                case ColorFormat.Yxy:
                    values[0] = (decimal)newColor.ToYxy().Y;
                    values[1] = (decimal)newColor.ToYxy().X;
                    values[2] = (decimal)newColor.ToYxy().Y;
                    break;
            }
            UpdateValues();
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
                ((NumericUpDown)this.Controls[index]).Value = values[index];
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
                case ColorFormat.AdobeRGB:
                    ResizeValues(3);
                    values = new decimal[] { 1M, 1M, 1M };
                    minValues = new decimal[] { 0, 0, 0 };
                    maxValues = new decimal[] { 255M, 255M, 255M };
                    ColorComboBox_ClientSizeChanged(null, EventArgs.Empty);
                    break;
                case ColorFormat.XYZ:
                    ResizeValues(3);
                    values = new decimal[] { 1M, 1M, 1M };
                    minValues = new decimal[] { 0, 0, 0 };
                    maxValues = new decimal[] { 150M, 100M, 150M };
                    break;
                case ColorFormat.Yxy:
                    ResizeValues(3);
                    values = new decimal[] { 1M, 1M, 1M };
                    minValues = new decimal[] { 0, 0, 0 };
                    maxValues = new decimal[] { 100M, 150M, 100M };
                    break;
            }
        }

        private void NumericUpDownKeyUp_Event(object sender, KeyEventArgs e)
        {
            if (this.decimalPlaces == 0)
                if ((e.KeyValue >= 0x30 && e.KeyValue <= 0x39) | (e.KeyValue >= 0x60 && e.KeyValue <= 0x69)) // numbers and num pad
                {
                    NumericUpDown n = (NumericUpDown)sender;
                    int index = this.Controls.IndexOf(n);

                    if (((int)n.Value).ToString().Length + 1 > ((int)maxValues[index]).ToString().Length)
                    {
                        if (index + 1 < this.Controls.Count)
                        {
                            ((NumericUpDown)this.Controls[index + 1]).Select();
                            ((NumericUpDown)this.Controls[index + 1]).Focus();
                        }
                    }
                }
        }

        private void ColorComboBox_ClientSizeChanged(object sender, EventArgs e)
        {
            CreateNumericUpDown();
        }

        private void ResizeValues(int newSize)
        {
            Array.Resize(ref values, newSize);
            Array.Resize(ref minValues, newSize);
            Array.Resize(ref maxValues, newSize);
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
                n.KeyUp += NumericUpDownKeyUp_Event;
                //n.MouseUp += Button_MouseUp;
                this.Controls.Add(n);
            }
            Refresh();
        }
    }
}
