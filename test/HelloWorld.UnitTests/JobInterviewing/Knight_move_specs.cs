using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            => OnBoard(square)
            ? Transforms.Select(tranform => new string(new[]
            {
                (char)(square[0] + tranform[0]),
                (char)(square[1] + tranform[1])
            })).Where(OnBoard)
            : Array.Empty<string>();

        private readonly int[][] Transforms = new[] 
        {
            new[] { -2, -1 },
            new[] { -1, -2 },
            new[] { -2, +1 },
            new[] { +1, -2 },

            new[] { +2, +1 },
            new[] { +1, +2 },
            new[] { +2, -1 },
            new[] { -1, +2 },
        };

        private bool OnBoard(string square)
            => square?.Length == 2
            && square[0] >= 'A' && square[0] <= 'H'
            && square[1] >= '1' && square[1] <= '8';
    }
}
