using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WinkingCat.HelperLibs
{
    public static class MathHelper
    {
        /// <summary>
        /// Clamps a number to the minimum and maximum value given.
        /// </summary>
        /// <typeparam name="T">Any comparable type.</typeparam>
        /// <param name="num">The number.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The given number between the min and max.</returns>
        public static T Clamp<T>(T num, T min, T max) where T : IComparable<T>
        {
            if (num.CompareTo(min) <= 0) return min;
            if (num.CompareTo(max) >= 0) return max;
            return num;
        }

        /// <summary>
        /// Clamps a number to the minimum value given.
        /// </summary>
        /// <typeparam name="T">Any comparable type.</typeparam>
        /// <param name="num">The number.</param>
        /// <param name="min">The minimum value.</param>
        /// <returns>The given number equal or above the min.</returns>
        public static T ClampMin<T>(T num, T min) where T : IComparable<T>
        {
            if (num.CompareTo(min) <= 0) return min;
            return num;
        }

        /// <summary>
        /// Clamps a number to the maximum value given.
        /// </summary>
        /// <typeparam name="T">Any comparable type.</typeparam>
        /// <param name="num">The number.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The given number equal or below the max.</returns>
        public static T ClampMax<T>(T num, T max) where T : IComparable<T>
        {
            if (num.CompareTo(max) >= 0) return max;
            return num;
        }

        public static int MakeOdd(int input, bool roundUp = true)
        {
            if (!IsEven(input))
                return input;
            
            if (roundUp)
                return input + 1;
            return input - 1;
        }

        public static bool IsEven(int number)
        {
            return (number % 2 == 0); //even number
        }
    }
}
