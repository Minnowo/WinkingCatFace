using System;
using System.IO;
using System.Windows.Forms;
using WinkingCat.HelperLibs;
using WinkingCat.Native;
using WinkingCat.Settings;

namespace WinkingCat.Controls
{
    public class LISTVIEW : ListView
    {
        public MouseButtons DragDropButton { get; set; } = MouseButtons.Left;
        public DragDropEffects DragDropEffects { get; set; } = DragDropEffects.Copy | DragDropEffects.Move;

        public bool autoFillColumn { get; set; } = true;

        private bool _isLeftClick = false;
        // private bool _isRightClick = false;

        public int SelectedIndex1 = -1;
        public int SelectedIndex2 = -1;

        public int SelectedItemsCount = 0;

        public LISTVIEW()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage, true);
            this.AllowDrop = true;
        }

        public void UpdateTheme()
        {
            this.SupportCustomTheme();
            this.BackColor = SettingsManager.MainFormSettings.backgroundColor;
            this.ForeColor = SettingsManager.MainFormSettings.textColor;
            Refresh();
        }

        public void DeselectAll()
        {
            this.SelectedIndices.Clear();
        }

        private void AdjustSelectedIndex(int count = -2)
        {
            if (count != -2)
            {
                SelectedItemsCount = this.SelectedIndices.Count;
            }
            else
            {
                SelectedItemsCount = count;
            }

            if (SelectedItemsCount < 1)
                return;

            if (this.SelectedIndex2 == this.SelectedIndices[0])
            {
                this.SelectedIndex1 = this.SelectedIndices[SelectedItemsCount - 1];
            }

            if (this.SelectedIndex1 == this.SelectedIndices[0])
            {
                this.SelectedIndex2 = this.SelectedIndices[SelectedItemsCount - 1];
            }

            OnSelectedIndexChanged(EventArgs.Empty);
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            if (!AllowDrop || e.Button != this.DragDropButton)
                return;

            string[] files = new string[this.SelectedIndices.Count];
            int c = 0;
            foreach (int i in this.SelectedIndices)
            {
                if (Items[i].Tag is FileInfo)
                {
                    files[c++] = ((FileInfo)Items[i].Tag).FullName;
                }
                else if (Items[i].Tag is DirectoryInfo)
                {
                    files[c++] = ((DirectoryInfo)Items[i].Tag).FullName;
                }
            }

            DoDragDrop(new DataObject(DataFormats.FileDrop, files), this.DragDropEffects);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            switch (e.Button)
            {
                case MouseButtons.Left:
                    _isLeftClick = true;
                    break;
                    /*case MouseButtons.Right:
                        _isRightClick = true;
                        break;*/
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            switch (e.Button)
            {
                case MouseButtons.Left:
                    _isLeftClick = false;
                    AdjustSelectedIndex();
                    break;
                    /*case MouseButtons.Right:
                        _isRightClick = false;
                        break;*/
            }
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

            if (!_isLeftClick)
                return;

            int count = this.SelectedIndices.Count;

            if (SelectedItemsCount == count)
                return;

            AdjustSelectedIndex(count);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            switch (e.KeyData)
            {
                case Keys.Up:
                case Keys.Down:
                    SelectedItemsCount = 1;
                    OnSelectedIndexChanged(EventArgs.Empty);
                    break;

                case Keys.Shift | Keys.Down:
                    SelectedItemsCount = this.SelectedIndices.Count;
                    OnSelectedIndexChanged(EventArgs.Empty);
                    break;

                case Keys.Shift | Keys.Up:
                    SelectedItemsCount = this.SelectedIndices.Count;
                    OnSelectedIndexChanged(EventArgs.Empty);
                    break;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyData)
            {
                case Keys.Up:
                case Keys.Down:
                    SelectedItemsCount = 1;
                    OnSelectedIndexChanged(EventArgs.Empty);
                    break;

                case Keys.Shift | Keys.Down:
                    this.SelectedIndices.Add(this.SelectedIndex1);
                    this.SelectedIndex1 = (this.SelectedIndex1 + 1).ClampMax(this.Items.Count);
                    break;

                case Keys.Shift | Keys.Up:
                    this.SelectedIndices.Add(this.SelectedIndex1);
                    this.SelectedIndex1 = (this.SelectedIndex1 - 1).ClampMin(0);
                    break;
            }
        }

        protected override void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
        {
            SelectedIndex1 = e.ItemIndex;
            SelectedIndex2 = e.ItemIndex;
            base.OnItemSelectionChanged(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (SettingsManager.MainFormSettings.forceColumnFill && m.Msg == (int)WindowsMessages.PAINT && !DesignMode)
            {
                if (Columns.Count != 0) // sizes the columns to fill the rest of the list box
                {
                    this.Columns[this.Columns.Count - 1].Width = -2;
                }
            }
            base.WndProc(ref m);
        }
    }
}
