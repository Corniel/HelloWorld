using NUnit.Framework;

namespace Rounding_specs
{
    public class Negative_decimals
    {
        [TestCase(123_456.78, +2, 123_456.78)]
        [TestCase(123_456.78, +1, 123_456.8)]
        [TestCase(123_456.78, +0, 123_457)]
        [TestCase(123_456.78, -1, 123_460)]
        [TestCase(123_456.78, -2, 123_500)]
        [TestCase(123_456.78, -3, 123_000)]
        [TestCase(123_456.78, -4, 120_000)]
        public void Round(decimal number, int decimals, decimal rounded)
        {
            Assert.AreEqual(rounded, Round(number, decimals));
        }

        public decimal Round(decimal number, int decimals)
        {
            return number;
        }
    }
}
