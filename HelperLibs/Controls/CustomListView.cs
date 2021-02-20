using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public class NoCheckboxListView : ListView
    {
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get
            {
                if (SelectedIndices.Count > 0)
                {
                    return SelectedIndices[0];
                }

                return -1;
            }
            set
            {
                UnselectAll();

                if (value > -1)
                {
                    ListViewItem lvi = Items[value];
                    lvi.EnsureVisible();
                    lvi.Selected = true;
                }
            }
        }
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public NoCheckboxListView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage, true);

            View = View.Details;
            BorderStyle = BorderStyle.FixedSingle;
            FullRowSelect = true;
            CheckBoxes = false;

            contextMenuStrip1 = new ContextMenuStrip();
            this.contextMenuStrip1.Items.AddRange(new ToolStripMenuItem[] {
            new ToolStripMenuItem(){Text = "copy", Name = "tsmiCopy"},
            new ToolStripMenuItem(){Text = "open", Name = "tsmiOpen"},
            new ToolStripMenuItem(){Text = "delete", Name = "tsmiDelete" },
});
            ContextMenuStrip = contextMenuStrip1;
            ContextMenuStrip.Opening += ContextMenuStrip_Opening;

        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (SelectedIndex != -1) 
            {
                if (Items[SelectedIndex].Selected)
                {
                    foreach (ToolStripMenuItem a in ContextMenuStrip.Items)
                    {
                        a.Enabled = true;
                    }
                }
                Console.WriteLine(SelectedIndex);
            } 
            else
            {
                foreach (ToolStripMenuItem a in ContextMenuStrip.Items)
                {
                    a.Enabled = false;
                }
            }
            if(this.Items.Count > 0)
            {
                foreach(ListViewItem item in Items)
                {
                    Console.WriteLine(item.Selected);
                }
            }
        }

        public void UnselectAll()
        {
            if (MultiSelect)
            {
                SelectedItems.Clear();
            }
        }
    }
}