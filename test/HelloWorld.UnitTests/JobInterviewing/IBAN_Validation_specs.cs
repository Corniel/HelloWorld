using NUnit.Framework;

namespace IBAN_Validation_specs
{
    public class Valid_for
    {
        [TestCase("BY13 NBRB 3600 9000 0000 2Z00 AB00")]
        [TestCase("NL20 INGB 0001 2345 67")]
        [TestCase("NL20INGB0001234567")]
        [TestCase("NL20ingb0001234567")]
        public void Country(string iban)
        {
            Assert.IsTrue(ValidIban(iban));
        }

        // iban => number
        // 0 => 0
        // 1 => 1
        // ...
        // 9 => 9
        // A => 10
        // B => 11
        // ...
        // 4 first chars at the end.
        //
        // valid: modulo(sum, 97) is 1
        // 
        // BY "4c,4n,16c"
        // NL "4a,10n"
        public bool ValidIban(string iban)
        {
            return false;
        }
    }
}
