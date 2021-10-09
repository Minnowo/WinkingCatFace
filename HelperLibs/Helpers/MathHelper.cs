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
        /// Makes the given int even.
        /// </summary>
        /// <param name="input">The number to make even.</param>
        /// <param name="roundUp">Should the number be increased by 1 to make it even.</param>
        /// <returns>The given numbber +- 1 to make even.</returns>
        public static int MakeEven(int input, bool roundUp = true)
        {
            if (IsEven(input))
                return input;
            
            if (roundUp)
                return input + 1;
            return input - 1;
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
