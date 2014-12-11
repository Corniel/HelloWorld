using System;

namespace HelloWorld.Ants
{
	public enum InstructionType
	{
		go,
		ready,
		end,
		None,
		player_seed,
		o,
		players,
		cols,
		rows,
		turntime,
		loadtime,
		viewradius2,
		attackradius2,
		spawnradius2,
		turn,
		turns,
		f,
		r,
		w,
		d,
		a,
		h,
	}
	public enum DirectionType
	{
		n, s, w, e, X,
	}
	public enum AntsAntType { }
	public class AntsColor
	{
		public const int None = -1;
		public const int Own = 0;
	}
	public class Truusje
	{
		public object Settings { get; set; }

		public int Turn { get; set; }
	}

	public class Map
	{
		internal static int[,] New<T1>(object p)
		{
			throw new NotImplementedException();
		}
	}
	public class AntsLoc
	{
		public int Row { get; set; }
		public int Col { get; set; }
	}
	public class AntsFood { }
	public class AntsWater { }
	public class AntsHill { }
	public class AntsAnt { }

	public class TruusjeCandidateMove
	{
		public TruusjeCandidateMove(AntsAnt ant, AntsLoc loc, DirectionType dir, int score, AntsAntType type, Strategy strategy) { }
	}
}
