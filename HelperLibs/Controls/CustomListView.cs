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

        public NoCheckboxListView()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage, true);

            View = View.Details;
            BorderStyle = BorderStyle.FixedSingle;
            FullRowSelect = true;
            CheckBoxes = false;
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