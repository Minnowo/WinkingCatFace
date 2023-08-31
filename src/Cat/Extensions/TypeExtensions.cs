using System;

namespace WinkingCat.HelperLibs
{
    public static class TypeExtensions
    {
        public static T Clamp<T>(this T input, T min, T max) where T : IComparable<T>
        {
            return MathHelper.Clamp(input, min, max);
        }

        public static T ClampMin<T>(this T input, T min) where T : IComparable<T>
        {
            return MathHelper.ClampMin(input, min);
        }

        public static T ClampMax<T>(this T input, T max) where T : IComparable<T>
        {
            return MathHelper.ClampMax(input, max);
        }
    }
}
