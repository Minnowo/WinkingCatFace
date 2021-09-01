using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace WinkingCat.HelperLibs
{
    public enum BitmapChanges
    {
        Inverted,
        SetGray,
        Cropped,
        Resized,
        //Dithered,
        TransparentFilled,
        RotatedLeft,
        RotatedRight,
        FlippedHorizontal,
        FlippedVirtical
    }

    public class BitmapUndo : IDisposable, IUndoable
    {
        public delegate void UndoEvent(BitmapChanges change);
        public static event UndoEvent UndoHappened;

        public delegate void RedoEvent(BitmapChanges change);
        public static event RedoEvent RedoHappened;

        public delegate void UpdateReferencesEvent();
        public static event UpdateReferencesEvent UpdateReferences;

        /// <summary>
        /// Fired after the current image has been modified via 
        /// <para>{ Undo, Redo, ResizeImage, CropImage, ConvertToGray, InvertBitmap, FlipVertical, FlipHorizontal, RotateRight, RotateLeft}</para>
        /// </summary>
        /// <param name="change"></param>
        public delegate void BitmapChangedEvent(BitmapChanges change);
        public event BitmapChangedEvent BitmapChanged;

        public int UndoCount
        {
            get { return undos.Count; }
        }

        public int RedoCount
        {
            get { return redos.Count; }
        }

        public BitmapChanges LastChange
        {
            get { return lastChange; }
            set { lastChange = value; }
        }
        private BitmapChanges lastChange;

        public Bitmap CurrentBitmap;

        private Stack<BitmapChanges> undos;
        private Stack<BitmapChanges> redos;
        private Stack<Bitmap> bitmapUndoHistoryData;
        private Stack<Bitmap> bitmapRedoHistoryData;

        public BitmapUndo()
        {
            undos = new Stack<BitmapChanges>();
            redos = new Stack<BitmapChanges>();
            bitmapUndoHistoryData = new Stack<Bitmap>();
            bitmapRedoHistoryData = new Stack<Bitmap>();
        }

        public BitmapUndo(Bitmap bmp)
        {
            undos = new Stack<BitmapChanges>();
            redos = new Stack<BitmapChanges>();
            bitmapUndoHistoryData = new Stack<Bitmap>();
            bitmapRedoHistoryData = new Stack<Bitmap>();

            CurrentBitmap = bmp;
        }


        /// <summary>
        /// Resize the current image and track the change. (will call a reference update event)
        /// </summary>
        /// <param name="newSize">The new size.</param>
        /// <param name="interp">The interpolation mode to use when resizing.</param>
        public void ResizeImage(Size newSize, InterpolationMode interp)
        {
            if (CurrentBitmap == null)
                return;

            TrackChange(BitmapChanges.Resized);

            using (Bitmap tmp = CurrentBitmap) 
            {
                CurrentBitmap = ImageHelper.GetResizedBitmap(tmp, newSize, interp);
            }

            OnUpdateReferences();
            OnBitmapChanged(BitmapChanges.Resized);
        }
        
        /// <summary>
        /// Crop the current bitmap and track the change. (will call a reference update event)
        /// </summary>
        /// <param name="crop"></param>
        public void CropImage(Rectangle crop)
        {
            if (CurrentBitmap == null)
                return;

            TrackChange(BitmapChanges.Cropped);

            ImageHelper.CropBitmapByRef(ref CurrentBitmap, crop);
            OnUpdateReferences(); // since CropBitmap creates a new bitmap we need to signal to update references

            OnBitmapChanged(BitmapChanges.Cropped);
        }

        /// <summary>
        /// Flips the current image in the y axis and tracks the change.
        /// </summary>
        public void FlipVertical()
        {
            if (CurrentBitmap == null)
                return;

            TrackChange(BitmapChanges.FlippedVirtical);

            ImageHelper.FlipVertical(CurrentBitmap);

            OnBitmapChanged(BitmapChanges.FlippedVirtical);
        }

        /// <summary>
        /// Flips the current image in the x axis and tracks the change.
        /// </summary>
        public void FlipHorizontal()
        {
            if (CurrentBitmap == null)
                return;

            TrackChange(BitmapChanges.FlippedHorizontal);

            ImageHelper.FlipHorizontal(CurrentBitmap);

            OnBitmapChanged(BitmapChanges.FlippedHorizontal);
        }

        /// <summary>
        /// Rotates the current image right by 90 and tracks the change.
        /// </summary>
        public void RotateRight()
        {
            if (CurrentBitmap == null)
                return;

            TrackChange(BitmapChanges.RotatedRight);

            ImageHelper.RotateRight(CurrentBitmap);

            OnBitmapChanged(BitmapChanges.RotatedRight);
        }

        /// <summary>
        /// Rotates the current image left by 90 and tracks the change.
        /// </summary>
        public void RotateLeft()
        {
            if (CurrentBitmap == null)
                return;

            TrackChange(BitmapChanges.RotatedLeft);

            ImageHelper.RotateLeft(CurrentBitmap);

            OnBitmapChanged(BitmapChanges.RotatedLeft);
        }

        /// <summary>
        /// Converts the current image to grayscale and tracks the change.
        /// </summary>
        public void ConvertToGray()
        {
            if (CurrentBitmap == null)
                return;

            TrackChange(BitmapChanges.SetGray);

            if (!ImageHelper.GrayscaleBitmapSafe(CurrentBitmap))
            {
                DisposeLastUndo();
                return;
            }
            OnBitmapChanged(BitmapChanges.SetGray);
        }

        /// <summary>
        /// Inverts the current image and tracks the change.
        /// </summary>
        public void InvertBitmap()
        {
            if (CurrentBitmap == null)
                return;

            TrackChange(BitmapChanges.Inverted);

            if (!ImageHelper.InvertBitmapSafe(CurrentBitmap))
            {
                DisposeLastUndo();
                return;
            }
            OnBitmapChanged(BitmapChanges.Inverted);
        }


        /// <summary>
        /// Disposes the current bitmap and replaces it with the nes bitmap.
        /// </summary>
        /// <param name="bmp">The new bitmap.</param>
        public void ReplaceBitmap(Bitmap bmp)
        {
            if (CurrentBitmap != null)
                CurrentBitmap.Dispose();

            CurrentBitmap = bmp;
        }

        /// <summary>
        /// Sets the CurrentBitmap to the given bitmap.
        /// </summary>
        /// <param name="bmp"></param>
        public void UpdateBitmapReferance(Bitmap bmp)
        {
            CurrentBitmap = bmp;
        }

        /// <summary>
        /// Keeps track of the change that is about to take place.
        /// </summary>
        /// <param name="change">The change that is going to take place.</param>
        public void TrackChange(BitmapChanges change)
        {
            lastChange = change;
            ClearRedos();
            undos.Push(change);

            switch (change)
            {
                // need to track history data
                case BitmapChanges.Cropped:
                case BitmapChanges.Resized:
                case BitmapChanges.SetGray:
                case BitmapChanges.TransparentFilled:
                    bitmapUndoHistoryData.Push(CurrentBitmap.CloneSafe());
                    break;

                // changes are easily undone and do not need to be kept in memory
                case BitmapChanges.Inverted:
                case BitmapChanges.RotatedLeft:
                case BitmapChanges.RotatedRight:
                case BitmapChanges.FlippedHorizontal:
                case BitmapChanges.FlippedVirtical:
                    break;
            }
        }


        /// <summary>
        /// Clears all the redos and disposes of any bitmap history kept.
        /// </summary>
        public void ClearRedos()
        {
            redos.Clear();
            for (int i = 0; i < bitmapRedoHistoryData.Count; i++)
            {
                bitmapRedoHistoryData.Pop().Dispose();
            }
        }

        /// <summary>
        /// Removes and disposes the last redo.
        /// </summary>
        public void DisposeLastRedo()
        {
            if (redos.Count < 1)
                return;

            BitmapChanges change = redos.Pop();

            // if the change being removed had any bitmap data stored dispose it
            switch (change)
            {
                case BitmapChanges.Cropped:
                case BitmapChanges.Resized:
                case BitmapChanges.SetGray:
                case BitmapChanges.TransparentFilled:
                    if (bitmapRedoHistoryData.Count < 1)
                        return;

                    bitmapRedoHistoryData.Pop().Dispose();
                    break;
            }
        }

        /// <summary>
        /// Redoes the last undo.
        /// </summary>
        public void Redo()
        {
            if (redos.Count < 1)
                return;

            BitmapChanges change = redos.Pop();
            undos.Push(change);
            lastChange = change;

            Bitmap bmp;

            switch (change)
            {
                // need to track history data
                case BitmapChanges.Cropped:
                case BitmapChanges.Resized:
                    bmp = bitmapRedoHistoryData.Pop();
                    bitmapUndoHistoryData.Push(CurrentBitmap.CloneSafe());
                    ReplaceBitmap(bmp);
                    OnUpdateReferences();
                    break;

                case BitmapChanges.SetGray:
                case BitmapChanges.TransparentFilled:
                    using (bmp = bitmapRedoHistoryData.Pop())
                    {
                        bitmapUndoHistoryData.Push(CurrentBitmap.CloneSafe());
                        ImageHelper.UpdateBitmap(CurrentBitmap, bmp);
                    }
                    break;

                // changes are easily undone and do not need to be kept in memory
                case BitmapChanges.Inverted:
                    ImageHelper.InvertBitmapSafe(CurrentBitmap);
                    break;
                case BitmapChanges.RotatedLeft:
                    ImageHelper.RotateLeft(CurrentBitmap);
                    break;
                case BitmapChanges.RotatedRight:
                    ImageHelper.RotateRight(CurrentBitmap);
                    break;
                case BitmapChanges.FlippedHorizontal:
                    ImageHelper.FlipHorizontal(CurrentBitmap);
                    break;
                case BitmapChanges.FlippedVirtical:
                    ImageHelper.FlipVertical(CurrentBitmap);
                    break;
            }
            OnRedo(change);
            OnBitmapChanged(change);
        }


        /// <summary>
        /// Clears all the undos and disposes of any bitmap history kept.
        /// </summary>
        public void ClearUndos()
        {
            undos.Clear();
            for (int i = 0; i < bitmapUndoHistoryData.Count; i++)
            {
                bitmapUndoHistoryData.Pop().Dispose();
            }
        }

        /// <summary>
        /// Removes and disposes the last undo.
        /// </summary>
        public void DisposeLastUndo()
        {
            if (undos.Count < 1)
                return;

            BitmapChanges change = undos.Pop();

            lastChange = undos.Peek();

            // if the change being removed had any bitmap data stored dispose it
            switch (change)
            {
                case BitmapChanges.Cropped:
                case BitmapChanges.Resized:
                case BitmapChanges.SetGray:
                case BitmapChanges.TransparentFilled:
                    if (bitmapUndoHistoryData.Count < 1)
                        return;

                    bitmapUndoHistoryData.Pop().Dispose();
                    break;
            }
        }

        /// <summary>
        /// Undoes the last tracked change.
        /// </summary>
        public void Undo()
        {
            if (undos.Count < 1)
                return;

            BitmapChanges change = undos.Pop();
            redos.Push(change);

            Bitmap bmp;
            switch (change)
            {
                // need to track history data
                case BitmapChanges.Cropped:
                case BitmapChanges.Resized:
                    bmp = bitmapUndoHistoryData.Pop();
                    bitmapRedoHistoryData.Push(CurrentBitmap.CloneSafe());
                    ReplaceBitmap(bmp);
                    OnUpdateReferences();
                    break;
                case BitmapChanges.SetGray:
                case BitmapChanges.TransparentFilled:
                    using (bmp = bitmapUndoHistoryData.Pop())
                    {
                        bitmapRedoHistoryData.Push(CurrentBitmap.CloneSafe());
                        ImageHelper.UpdateBitmap(CurrentBitmap, bmp);
                    }
                    break;

                // changes are easily undone and do not need to be kept in memory
                case BitmapChanges.Inverted:
                    ImageHelper.InvertBitmapSafe(CurrentBitmap);
                    break;
                case BitmapChanges.RotatedLeft:
                    ImageHelper.RotateRight(CurrentBitmap);
                    break;
                case BitmapChanges.RotatedRight:
                    ImageHelper.RotateLeft(CurrentBitmap);
                    break;
                case BitmapChanges.FlippedHorizontal:
                    ImageHelper.FlipHorizontal(CurrentBitmap);
                    break;
                case BitmapChanges.FlippedVirtical:
                    ImageHelper.FlipVertical(CurrentBitmap);
                    break;
            }
            OnUndo(change);
            OnBitmapChanged(change);
        }


        /// <summary>
        /// Clears and disposes of both the undos and redos.
        /// </summary>
        public void ClearHistory()
        {
            ClearUndos();
            ClearRedos();
        }

        /// <summary>
        /// Dispose of the undos, redos, and the current bitmap.
        /// </summary>
        public void Dispose()
        {
            ClearHistory();
            if (CurrentBitmap != null)
                CurrentBitmap.Dispose();
            CurrentBitmap = null;
        }


        private void OnBitmapChanged(BitmapChanges change)
        {
            if (BitmapChanged != null)
                BitmapChanged(change);
        }

        private void OnUndo(BitmapChanges change)
        {
            if (UndoHappened != null)
                UndoHappened(change);
        }

        private void OnRedo(BitmapChanges change)
        {
            if (RedoHappened != null)
                RedoHappened(change);
        }

        private void OnUpdateReferences()
        {
            if (UpdateReferences != null)
                UpdateReferences();
        }
    }
}
