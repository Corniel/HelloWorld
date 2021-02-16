using NUnit.Framework;
using System;
using System.Linq;

namespace Base32_specs
{
    public class Deserialize
    {
        [TestCase("BQ", 12)]
        [TestCase("BQRA", 12, 34)]
        [TestCase("BQRDQTS2N55YNEM4", 12, 34, 56, 78, 90, 111, 123, 134, 145, 156)]
        [TestCase("THEQUICKBROWNFOXJUMBSOVERTHELAZYDOG2345674", 153, 201, 010, 032, 074, 012, 093, 102, 149, 215, 077, 024, 025, 058, 164, 140, 206, 069, 131, 056, 027, 141, 173, 243, 190, 255)]
        public void String(string base32, params int[] ints)
        {
            var bytes = ints.Select(v => (byte)v).ToArray();
            CollectionAssert.AreEqual(bytes, FromBase32(base32));
        }

        // ABCDEFGHIJKLMNOPQRSTUVWXYZ234567
        // A => 00000
        // B => 00001
        // ...
        // 7 => 11111
        private static byte[] FromBase32(string base32)
        {
            return Array.Empty<byte>();
        }
    }
}
