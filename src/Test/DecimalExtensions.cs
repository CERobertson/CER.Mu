namespace CER.Test.Extensions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    public static class DecimalExtensions
    {
        public static void IsEqualTo(this decimal expected, decimal actual, int rounding_to_decimal = 0)
        {
            Assert.AreEqual(expected, Math.Round(actual, rounding_to_decimal));
        }

        public static void SumsToUnity(this decimal[] v)
        {
            var result = 0.0M;
            for (int i = 0; i < v.Length; i++)
            {
                result += v[i];
            }
            Assert.AreEqual(1.0M, Math.Round(result,27));
        }
    }
}
