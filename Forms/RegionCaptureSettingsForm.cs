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

            foreach(InRegionTasks task in Enum.GetValues(typeof(InRegionTasks)))
            {
                combobMouseMiddleClickAction.Items.Add(task);
                combobMouseRightClickAction.Items.Add(task);
                combobXButton1ClickAction.Items.Add(task);
                combobXButton2ClickAction.Items.Add(task);
            }

            combobMouseMiddleClickAction.SelectedItem = RegionCaptureOptions.onMouseMiddleClick;
            combobMouseRightClickAction.SelectedItem = RegionCaptureOptions.onMouseRightClick;
            combobXButton1ClickAction.SelectedItem = RegionCaptureOptions.onXButton1Click;
            combobXButton2ClickAction.SelectedItem = RegionCaptureOptions.onXButton2Click;

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

        #region ComboBox ValueChanged
        private void CombobMouseMiddleClickAction_SelectionChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.onMouseMiddleClick = (InRegionTasks)combobMouseMiddleClickAction.SelectedItem;
        }
        private void CombobMouseRightClickAction_SelectionChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.onMouseRightClick = (InRegionTasks)combobMouseRightClickAction.SelectedItem;
        }
        private void CombobXButton1ClickAction_SelectionChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.onXButton1Click = (InRegionTasks)combobXButton1ClickAction.SelectedItem;
        }
        private void CombobXButton2ClickAction_SelectionChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.onXButton2Click = (InRegionTasks)combobXButton2ClickAction.SelectedItem;
        }
        #endregion

        #region NumericUpDown ValueChanged
        private void NudMagnifierPixelCount_ValueChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.magnifierPixelCount = (int)nudMagnifierPixelCount.Value;
        }

        private void NudMagnifierPixelSize_ValueChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.magnifierPixelSize = (int)nudMagnifierPixelSize.Value;
        }

        private void NudMagnifierZoomScale_ValueChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.magnifierZoomScale = (float)nudMagnifierZoomScale.Value;
            nudMagnifierZoomLevel.Increment = nudMagnifierZoomScale.Value;
        }

        private void NudMagnifierZoomLevel_ValueChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.magnifierZoomLevel = (float)nudMagnifierZoomLevel.Value;
        }
        #endregion

        #region Checkbox CheckChanged

        private void CbDrawScreenWideCrosshair_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawCrossHair = cbDrawScreenWideCrosshair.Checked;
        }

        private void CbDimBackground_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.dimBackground = cbDimBackground.Checked;
        }

        private void CbDrawInfoText_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawInfoText = cbDrawInfoText.Checked;
        }

        private void CbUpdateOnMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.updateOnMouseMove = cbUpdateOnMouseMove.Checked;
        }

        private void CbDrawMarchingAnts_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.marchingAnts = cbDrawMarchingAnts.Checked;
        }

        private void CbDrawMagnifier_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawMagnifier = cbDrawMagnifier.Checked;
        }

        private void CbDrawMagnifierCrosshair_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawMagnifierCrosshair = cbDrawMagnifierCrosshair.Checked;
        }

        private void CbDrawMagnifierGrid_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawMagnifierGrid = cbDrawMagnifierGrid.Checked;
        }

        private void CbDrawMagnifierBorder_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.drawMagnifierBorder = cbDrawMagnifierBorder.Checked;
        }

        private void CbCenterMagnifierOnMouse_CheckedChanged(object sender, EventArgs e)
        {
            RegionCaptureOptions.tryCenterMagnifier = cbCenterMagnifierOnMouse.Checked;
        }
#endregion
    }
}
