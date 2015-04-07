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
    }
}
