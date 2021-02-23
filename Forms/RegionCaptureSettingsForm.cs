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
    public partial class RegionCaptureSettingsForm : Form
    {
        public RegionCaptureSettingsForm()
        {
            InitializeComponent();
            cbDrawScreenWideCrosshair.Checked = RegionCaptureOptions.drawCrossHair;
            cbDimBackground.Checked = RegionCaptureOptions.dimBackground;
            cbDrawInfoText.Checked= RegionCaptureOptions.drawInfoText;
            cbUpdateOnMouseMove.Checked = RegionCaptureOptions.updateOnMouseMove;
            cbDrawMarchingAnts.Checked = RegionCaptureOptions.marchingAnts;
            cbDrawMagnifier.Checked = RegionCaptureOptions.drawMagnifier;
            cbDrawMagnifierCrosshair.Checked = RegionCaptureOptions.drawMagnifierCrosshair;
            cbDrawMagnifierGrid.Checked = RegionCaptureOptions.drawMagnifierGrid;
            cbDrawMagnifierBorder.Checked = RegionCaptureOptions.drawMagnifierBorder;
            cbCenterMagnifierOnMouse.Checked = RegionCaptureOptions.tryCenterMagnifier;

            nudMagnifierPixelCount.Value = RegionCaptureOptions.magnifierPixelCount;
            nudMagnifierPixelSize.Value = RegionCaptureOptions.magnifierPixelSize;
            nudMagnifierZoomScale.Value = (decimal)RegionCaptureOptions.magnifierZoomScale;
            nudMagnifierZoomLevel.Value = (decimal)RegionCaptureOptions.magnifierZoomLevel;
            nudMagnifierZoomLevel.Increment = nudMagnifierZoomScale.Value;
        }

        #region NumericUpDown ValueChanged
        private void nudMagnifierPixelCount_ValueChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.magnifierPixelCount = (int)nudMagnifierPixelCount.Value;
        }

        private void nudMagnifierPixelSize_ValueChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.magnifierPixelSize = (int)nudMagnifierPixelSize.Value;
        }

        private void nudMagnifierZoomScale_ValueChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.magnifierZoomScale = (float)nudMagnifierZoomScale.Value;
            nudMagnifierZoomLevel.Increment = nudMagnifierZoomScale.Value;
        }

        private void nudMagnifierZoomLevel_ValueChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.magnifierZoomLevel = (float)nudMagnifierZoomLevel.Value;
        }
        #endregion

        #region Checkbox CheckChanged

        private void cbDrawScreenWideCrosshair_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawCrossHair = cbDrawScreenWideCrosshair.Checked;
        }

        private void cbDimBackground_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.dimBackground = cbDimBackground.Checked;
        }

        private void cbDrawInfoText_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawInfoText = cbDrawInfoText.Checked;
        }

        private void cbUpdateOnMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.updateOnMouseMove = cbUpdateOnMouseMove.Checked;
        }

        private void cbDrawMarchingAnts_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.marchingAnts = cbDrawMarchingAnts.Checked;
        }

        private void cbDrawMagnifier_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawMagnifier = cbDrawMagnifier.Checked;
        }

        private void cbDrawMagnifierCrosshair_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawMagnifierCrosshair = cbDrawMagnifierCrosshair.Checked;
        }

        private void cbDrawMagnifierGrid_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawMagnifierGrid = cbDrawMagnifierGrid.Checked;
        }

        private void cbDrawMagnifierBorder_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawMagnifierBorder = cbDrawMagnifierBorder.Checked;
        }

        private void cbCenterMagnifierOnMouse_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.tryCenterMagnifier = cbCenterMagnifierOnMouse.Checked;
        }
#endregion
    }
}
