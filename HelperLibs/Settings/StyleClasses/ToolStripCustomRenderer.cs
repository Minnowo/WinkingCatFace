﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WinkingCat.HelperLibs
{
    public class ToolStripCustomRenderer : ToolStripProfessionalRenderer
    {
        public ToolStripCustomRenderer() : base(new CustomColorTable())
        {
            RoundedEdges = false;
        }

        public ToolStripCustomRenderer(CustomColorTable customColorTable) : base(customColorTable)
        {
            RoundedEdges = false;
        }


        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item is ToolStripMenuItem tsmi && tsmi.Checked)
            {
                e.TextFont = new Font(tsmi.Font, FontStyle.Bold);
            }
            e.Item.ForeColor = SettingsManager.MainFormSettings.textColor;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (e.Item is ToolStripDropDownButton tsddb && tsddb.Owner is ToolStrip)
            {
                e.Direction = ArrowDirection.Right;
            }
            e.ArrowColor = SettingsManager.MainFormSettings.textColor;
            base.OnRenderArrow(e);
        }
    }
}
