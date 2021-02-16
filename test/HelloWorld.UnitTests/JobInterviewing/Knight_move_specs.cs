using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Knight_move_specs
{
    public class Moves_to
    {
        [TestCase("H1", "F2", "G3")]
        [TestCase("C3", "B1", "D1", "A2", "E2", "A4", "E4", "B5", "D5")]
        public void Squares(string square, params string[] targets)
        {
            CollectionAssert.AreEquivalent(targets, KnightMoves(square));
        }

        private IEnumerable<string> KnightMoves(string square)
        {
            return Array.Empty<string>();
        }
    }
}
