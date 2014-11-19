﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
	public class RomanNumerals
	{
		/// <summary>Converts an System.Int32 to Roman number strings.
		/// <remarks>http://en.wikipedia.org/wiki/Roman_numerals</remarks>
		/// </summary>
		/// <param name="number">Number to convert.</param>
		/// <returns>Formatted Roman number.</returns>
		public static string ToFormattedRomanNumber(int number)
		{
			if (number <= 0 || number > 3999)
			{
				throw new ArgumentOutOfRangeException("With the default ASCII set only Roman numerals between 1 and 4000 can be represented.");
			}
			//                 1,   5,   10,  50,  100, 500, 1000.
			string[] order = { "I", "V", "X", "L", "C", "D", "M" };
			int pointer = 0;
			int digits = number;
			string result = string.Empty;

			while (digits > 0)
			{
				int digit = digits % 10;
				switch (digit)
				{
					case 0: break;
					case 1: result = string.Format(
						"{1}{0}", result, order[pointer]);
						break;
					case 2: result = string.Format(
						"{1}{1}{0}", result, order[pointer]);
						break;
					case 3: result = string.Format(
						"{1}{1}{1}{0}", result, order[pointer]);
						break;
					case 4: result = string.Format(
						"{1}{2}{0}", result, order[pointer], order[pointer + 1]);
						break;
					case 5: result = string.Format(
						"{1}{0}", result, order[pointer + 1]);
						break;
					case 6: result = string.Format(
						"{2}{1}{0}", result, order[pointer], order[pointer + 1]);
						break;
					case 7: result = string.Format(
						"{2}{1}{1}{0}", result, order[pointer], order[pointer + 1]);
						break;
					case 8: result = string.Format(
						"{2}{1}{1}{1}{0}", result, order[pointer], order[pointer + 1]);
						break;
					case 9: result = string.Format(
						"{1}{2}{0}", result, order[pointer], order[pointer + 2]);
						break;
				}
				digits = digits / 10;
				pointer = pointer + 2;
			}
			return result;
		}
	}
}
