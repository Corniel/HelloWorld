using NUnit.Framework;
using Condition = HelloWorld.Sonar.Condition<HelloWorld.Sonar.BaseContext>;

namespace Specs
{
    public class Conditions
    {
        public static readonly Condition True = (Condition)(context => true);
        public static readonly Condition False = (Condition)(context => false);

        [Test]
        public void True_and_True_is_True()
        {
            var combined = True & True;
            Assert.That(combined.Invoke(null), Is.True);
        }

        [Test]
        public void True_and_False_is_False()
        {
            var combined = True & False;
            Assert.That(combined.Invoke(null), Is.False);
        }

        [Test]
        public void False_or_False_is_False()
        {
            var combined = False | False;
            Assert.That(combined.Invoke(null), Is.False);
        }

        [Test]
        public void True_or_False_is_True()
        {
            var combined = True | False;
            Assert.That(combined.Invoke(null), Is.True);
        }

        [Test]
        public void Not_True_is_False()
        {
            var negated = ~True;
            Assert.That(negated.Invoke(null), Is.False);
        }
    }
}
