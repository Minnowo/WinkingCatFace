using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Globalization;

namespace WinkingCat.HelperLibs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public int X
        {
            get
            {
                return Left;
            }
            set
            {
                Right -= Left - value;
                Left = value;
            }
        }

        public int Y
        {
            get
            {
                return Top;
            }
            set
            {
                Bottom -= Top - value;
                Top = value;
            }
        }

        public int Width
        {
            get
            {
                return Right - Left;
            }
            set
            {
                Right = value + Left;
            }
        }

        public int Height
        {
            get
            {
                return Bottom - Top;
            }
            set
            {
                Bottom = value + Top;
            }
        }

        public Point Location
        {
            get
            {
                return new Point(Left, Top);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Size Size
        {
            get
            {
                return new Size(Width, Height);
            }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
        {
        }

        public static implicit operator Rectangle(RECT r)
        {
            return new Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(Rectangle r)
        {
            return new RECT(r);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
            {
                return Equals((RECT)obj);
            }

            if (obj is Rectangle)
            {
                return Equals(new RECT((Rectangle)obj));
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ((Rectangle)this).GetHashCode();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static explicit operator Point(POINT p)
        {
            return new Point(p.X, p.Y);
        }

        public static explicit operator POINT(Point p)
        {
            return new POINT(p.X, p.Y);
        }

    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IconInfo
    {
        /// <summary>
        /// Specifies whether this structure defines an icon or a cursor. A value of TRUE specifies an icon; FALSE specifies a cursor.
        /// </summary>
        public bool fIcon;

        /// <summary>
        /// The x-coordinate of a cursor's hot spot. If this structure defines an icon, the hot spot is always in the center of the icon, and this member is ignored.
        /// </summary>
        public int xHotspot;

        /// <summary>
        /// The y-coordinate of the cursor's hot spot. If this structure defines an icon, the hot spot is always in the center of the icon, and this member is ignored.
        /// </summary>
        public int yHotspot;

        /// <summary>
        /// The icon bitmask bitmap. If this structure defines a black and white icon, this bitmask is formatted so that the upper half is the icon AND bitmask and the lower half is the icon XOR bitmask. Under this condition, the height should be an even multiple of two. If this structure defines a color icon, this mask only defines the AND bitmask of the icon.
        /// </summary>
        public IntPtr hbmMask;

        /// <summary>
        /// A handle to the icon color bitmap. This member can be optional if this structure defines a black and white icon. The AND bitmask of hbmMask is applied with the SRCAND flag to the destination; subsequently, the color bitmap is applied (using XOR) to the destination by using the SRCINVERT flag.
        /// </summary>
        public IntPtr hbmColor;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CursorInfo
    {
        /// <summary>
        /// The size of the structure, in bytes. The caller must set this to sizeof(CURSORINFO).
        /// </summary>
        public int cbSize;

        /// <summary>
        /// The cursor state. This parameter can be one of the following values:
        /// 0 (The cursor is hidden.)
        /// CURSOR_SHOWING 0x00000001 (The cursor is showing.)
        /// CURSOR_SUPPRESSED 0x00000002 (Windows 8: The cursor is suppressed.This flag indicates that the system is not drawing the cursor because the user is providing input through touch or pen instead of the mouse.)
        /// </summary>
        public int flags;

        /// <summary>
        /// A handle to the cursor.
        /// </summary>
        public IntPtr hCursor;

        /// <summary>
        /// A structure that receives the screen coordinates of the cursor.
        /// </summary>
        public Point ptScreenPos;
    }


}
