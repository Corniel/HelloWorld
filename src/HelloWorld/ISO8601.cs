using System;

namespace HelloWorld
{
	/// Represents a date as specified by ISO 8601 week date.
	///
	/// See: http://en.wikipedia.org/wiki/ISO_8601
	/// and: http://en.wikipedia.org/wiki/ISO_week_date
	///
	public struct Iso8601WeekDate
	{
		private int m_Day;
		private int m_Year;
		private int m_Week;
		private DateTime m_Date;

		/// Initializes a new instance of the Tjip.Iso8601WeekDate structure to the specified System.DateTime.
		/// The date of the ISO 8601 WeekDate.
		public Iso8601WeekDate(DateTime date)
		{
			// Only the date will be available.
			m_Date = date.Date;
			// Set the year.
			m_Year = date.Year;
			// The day is okay by default, Unless its Sunday (int value = 0)...
			m_Day = (date.DayOfWeek == DayOfWeek.Sunday) ? 7 : (int)date.DayOfWeek;

			// Now the week number.
			DateTime startdate = GetFirstDayOfFirtWeekOfYear(date.Year);
			DateTime enddate = GetFirstDayOfFirtWeekOfYear(date.Year + 1);
			// The date is member of a week in the next year.
			if (m_Date >= enddate)
			{
				startdate = enddate;
				m_Year++;
			}
			// The date is member of a week in the previous year.
			if (m_Date < startdate)
			{
				startdate = GetFirstDayOfFirtWeekOfYear(date.Year - 1);
				m_Year--;
			}
			// Day of the week.
			int dayofyear = (m_Date - startdate).Days;

			// The week number is not zero based.
			m_Week = dayofyear / 7 + 1;
		}

		/// Gets the date component of this instance.
		public DateTime Date { get { return m_Date; } }
		/// Gets the year component of the date represented by this instance.
		public int Year { get { return m_Year; } }
		/// Gets the week component of the date represented by this instance.
		public int Week { get { return m_Week; } }
		/// Gets the day component of the date represented by this instance.
		public int Day { get { return m_Day; } }

		/// Gets the date of the first day of the first week of the year.
		///
		/// Source: http://en.wikipedia.org/wiki/ISO_8601
		///
		/// There are mutually equivalent descriptions of week 01:
		/// - the week with the year's first Thursday in it (the formal ISO definition),
		/// - the week with 4 January in it,
		/// - the first week with the majority (four or more) of its days in the starting year,
		/// - the week starting with the Monday in the period 29 December â€“ 4 January.
		///
		public static DateTime GetFirstDayOfFirtWeekOfYear(int year)
		{
			DateTime start = new DateTime(year, 01, 04);
			while (start.DayOfWeek != DayOfWeek.Monday)
			{
				start = start.AddDays(-1);
			}
			return start;
		}

		/// Represents the Tjip.Iso8601WeekDate as System.String.
		public override string ToString()
		{
			return ToString("YYYY-Www-D");
		}
		/// Represents the Tjip.Iso8601WeekDate as System.String.
		/// The format.  
		///
		/// Representations of the following formatting are allowed:
		/// - YYYYWww
		/// - YYYY-Www
		/// - YYYYWwwD
		/// - YYYY-Www-D
		///
		/// [YYYY] indicates the ISO week-numbering year which is slightly different
		/// to the calendar year (see below).
		///
		/// [Www] is the week number prefixed by the letter 'W', from W01 through W53.
		///
		/// [D] is the weekday number, from 1 through 7, beginning with
		///
		/// Monday and ending with Sunday. This form is popular in the
		/// manufacturing industries.
		///
		public string ToString(string format)
		{
			switch (format)
			{
				case "YYYYWww": return string.Format("{0}W{1:00}", this.Year, this.Week, this.Day);
				case "YYYY-Www": return string.Format("{0}-W{1:00}", this.Year, this.Week, this.Day);
				case "YYYYWwwD": return string.Format("{0}W{1:00}{2}", this.Year, this.Week, this.Day);
				case "YYYY-Www-D": return string.Format("{0}-W{1:00}-{2}", this.Year, this.Week, this.Day);
				default: throw new NotSupportedException(string.Format("The format '{0}' is not supported.", format));
			}
		}
		/// Returns the hash code for this instance.
		///
		/// A 32-bit signed integer that is the hash code for this instance.
		///
		public override int GetHashCode()
		{
			// It should be fast, so shift.
			//       ..3.....6............14 = 23 bit
			// bits: DDDWWWWWWYYYYYYYYYYYYYY
			int hash = this.Day | (this.Week >> 3) | (this.Year >> 9);
			return hash;
		}
	}
}

namespace HelloWorld.Extensions
{
	public static class DateTimeExtensions
	{
		/// Returns ISO WeekNumber (1-53) for a given year.
		/// The datetime.
		public static int ISOWeekNumberOld(this System.DateTime dt)
		{
			// Set Year
			int yyyy = dt.Year;
			// Set Month
			int mm = dt.Month;
			// Set Day
			int dd = dt.Day;
			// Declare other required variables
			int DayOfYearNumber;
			int Jan1WeekDay;
			int WeekNumber = 0, WeekDay;
			int i, j, k, l, m, n;
			int[] Mnth = new int[12] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
			int YearNumber;
			// Set DayofYear Number for yyyy mm dd
			DayOfYearNumber = dd + Mnth[mm - 1];
			// Increase of Dayof Year Number by 1, if year is leapyear and month is february
			if ((DateTime.IsLeapYear(yyyy) == true) && (mm == 2))
				DayOfYearNumber += 1;
			// Find the Jan1WeekDay for year
			i = (yyyy - 1) % 100;
			j = (yyyy - 1) - i;
			k = i + i / 4;
			Jan1WeekDay = 1 + (((((j / 100) % 4) * 5) + k) % 7);
			// Calcuate the WeekDay for the given date
			l = DayOfYearNumber + (Jan1WeekDay - 1);
			WeekDay = 1 + ((l - 1) % 7);
			// Find if the date falls in YearNumber set WeekNumber to 52 or 53
			if ((DayOfYearNumber <= (8 - Jan1WeekDay)) && (Jan1WeekDay > 4))
			{
				YearNumber = yyyy - 1;
				if ((Jan1WeekDay == 5) || ((Jan1WeekDay == 6) && (Jan1WeekDay > 4)))
					WeekNumber = 53;
				else
					WeekNumber = 52;
			}
			else
				YearNumber = yyyy;
			// Set WeekNumber to 1 to 53 if date falls in YearNumber
			if (YearNumber == yyyy)
			{
				if (DateTime.IsLeapYear(yyyy) == true)
					m = 366;
				else
					m = 365;
				if ((m - DayOfYearNumber) < (4 - WeekDay)) { YearNumber = yyyy + 1; WeekNumber = 1; }
			} if (YearNumber == yyyy)
			{
				n = DayOfYearNumber + (7 - WeekDay) + (Jan1WeekDay - 1); WeekNumber = n / 7; if (Jan1WeekDay > 4)
					WeekNumber -= 1;
			}
			return (WeekNumber);
		}

		public static Iso8601WeekDate ToIso8601WeekDate(this DateTime dt)
		{
			return new Iso8601WeekDate(dt);
		}

		/// Returns the (ISO 8601) number of the week.
		/// The date time.
		/// The (ISO 8601) number of the week.
		///
		///  Source: http://en.wikipedia.org/wiki/ISO_8601
		///
		public static int ISO8601WeekNumber(this DateTime dt)
		{
			Iso8601WeekDate weekdate = new Iso8601WeekDate(dt);
			return weekdate.Week;
		}
	}
}
