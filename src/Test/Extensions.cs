namespace CER.Test.Extensions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    public static class DecimalExtensions
    {
        public static void Assert_AboutEqual(this decimal expected, decimal actual)
        {
            var rounding_to_decimal = expected.ToString().Split('.')[1].Length;
            Assert.AreEqual(expected, Math.Round(actual, rounding_to_decimal));
        }

        public static void AssertSumsToUnity(this decimal[] v)
        {
            var result = 0.0M;
            for (int i = 0; i < v.Length; i++)
            {
                result += v[i];
            }
            Assert.AreEqual(1.0M, Math.Round(result, 27));
        }

        public static string ToFavoriteString(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
