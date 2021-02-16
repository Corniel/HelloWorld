using NUnit.Framework;

namespace Rounding_specs
{
    public class Negative_decimals
    {
        [TestCase(123456.78, -2, 123456.78)]
        [TestCase(123456.78, -1, 123456.8)]
        [TestCase(123456.78, +0, 123457)]
        [TestCase(123456.78, -1, 123460)]
        [TestCase(123456.78, -2, 123500)]
        [TestCase(123456.78, -3, 123000)]
        [TestCase(123456.78, -4, 120000)]
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
