using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloWorld.UnitTests
{
    public class FizzBuzzTest
    {
        [TestCaseSource(nameof(Records))]
        public void StringConcat(int number, string expected)
        {
            AssertFx(expected, number, FizzBuzz.StringConcat);
        }

        [TestCaseSource(nameof(Records))]
        public void WithSwitch(int number, string expected)
        {
            AssertFx(expected, number, FizzBuzz.WithSwitch);
        }

        [TestCaseSource(nameof(Records))]
        public void WithConditionalOperator(int number, string expected)
        {
            AssertFx(expected, number, FizzBuzz.WithConditionalOperator);
        }

        [TestCaseSource(nameof(Records))]
        public void N19142723(int number, string expected)
        {
            AssertFx(expected, number, FizzBuzz.N19142723);
        }

        private static void AssertFx(string expected, int number,  Func<int, string> fx)
        {
            Assert.AreEqual(expected, fx(number));
        }

        private static IEnumerable<object[]> Records()
        {
            return new Dictionary<int, string> {
                { 00, "FizzBuzz" },
                { 01, "1" },
                { 02, "2" },
                { 03, "Fizz" },
                { 04, "4" },
                { 05, "Buzz" },
                { 06, "Fizz" },
                { 07, "7" },
                { 08, "8" },
                { 09, "Fizz" },
                { 10, "Buzz" },
                { 11, "11" },
                { 12, "Fizz" },
                { 13, "13" },
                { 14, "14" },
                { 15, "FizzBuzz" },
                { 50, "Buzz" },
                { 99, "Fizz" },
                { 225, "FizzBuzz" },
                { 998, "998" },
            }
            .Select(kvp => new object[] { kvp.Key, kvp.Value });
        }
    }
}
