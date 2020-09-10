using System;

namespace Dannys.Framework
{
	/// <summary>
	/// Birthday object that allow nullable year, month or day
	/// </summary>
	public class Birthday : IEquatable<Birthday>, IComparable<Birthday>
	{
		internal const short DummyYear = 32767;
		short year = DummyYear;
		byte month = 0;
		byte day = 0;

		public Birthday() { }

		public Birthday(short? year, byte? month, byte? day)
		{
			Year = year;
			Month = month;
			Day = day;
		}

		/// <summary>
		/// Year valid from -32767 to 32766
		/// </summary>
		public short? Year
		{
			get { return year == DummyYear ? default(short?) : year; }
			set { if (value == DummyYear) throw new ArgumentOutOfRangeException("Year"); year = value ?? DummyYear; validateDate(); }
		}

		/// <summary>
		/// Month of birthday (1 to 12)
		/// </summary>
		public byte? Month
		{
			get { return month == 0 ? default(byte?) : month; }
			set { if (value > 12) throw new ArgumentOutOfRangeException("Month"); month = value ?? 0; validateDate(); }
		}

		/// <summary>
		/// Day of birthday (1 to 31)
		/// </summary>
		public byte? Day
		{
			get { return day == 0 ? default(byte?) : day; }
			set { if (value > 31) throw new ArgumentOutOfRangeException("Day"); day = value ?? 0; validateDate(); }
		}

		/// <summary>
		/// Static helper to convert integer to birthday
		/// </summary>
		/// <param name="repr"></param>
		/// <returns></returns>
		public static Birthday FromIntegerRepresentation(int repr)
		{
			Birthday birthday = new Birthday();
			birthday.SetIntegerRepresentation(repr);
			return birthday;
		}

		/// <summary>
		/// Convert value to integer
		/// </summary>
		/// <returns></returns>
		public int GetIntegerRepresentation()
		{
			return ((Year ?? DummyYear) << 16) | ((Month ?? 0) << 4) | ((Day ?? 0) & 0xffff);
		}

		/// <summary>
		/// Set birthday by integer representation
		/// </summary>
		/// <param name="value"></param>
		public void SetIntegerRepresentation(int value)
		{
			year = (short)(value / 65536);
			month = (byte)(value % 65536 / 256);
			day = (byte)(value % 256);
		}

		public override string ToString()
		{
			return $"{Day?.ToString() ?? "D"}/{Month?.ToString() ?? "M"}/{yearToString() ?? "Y"}";
		}

		public override bool Equals(object obj)
		{
			if (obj != null && obj is Birthday)
			{
				return (obj as Birthday).GetIntegerRepresentation() == GetIntegerRepresentation();
			}
			else
				return false;
		}

		public override int GetHashCode()
		{
			return GetIntegerRepresentation();
		}

		public bool Equals(Birthday other)
		{
			if (other != null)
			{
				return other.GetIntegerRepresentation() == GetIntegerRepresentation();
			}
			else
				return false;
		}

		public int CompareTo(Birthday other)
		{
			if (other == null) throw new ArgumentNullException("other");

			if (year == other.year)
			{
				return GetIntegerRepresentation().CompareTo(other.GetIntegerRepresentation());
			}
			else
			{
				if (year == DummyYear)
					return -1;
				else if (other.year == DummyYear)
					return 1;
				return year.CompareTo(other.year);
			}
		}

		string yearToString()
		{
			return Year.HasValue ? (Year > 0 ? Year.ToString() : (-Year).ToString() + "BC") : null;
		}

		void validateDate()
		{
			if (month > 12) throw new ArgumentOutOfRangeException("Month");
			if (day > 31) throw new ArgumentOutOfRangeException("Day");
			if (year == 0) throw new ArgumentOutOfRangeException("Year", "Year must not be 0");

			if (month > 0 && day > 0)
			{
				int maxDay = DaysInMonth(Year ?? -32767, month);
				if (day > maxDay) throw new ArgumentOutOfRangeException("Day");
			}
		}

		/// <summary>
		/// DaysInMonth for larger date range
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <returns></returns>
		public static int DaysInMonth(short year, byte month)
		{
			if (month > 12 || month < 1)
				throw new ArgumentOutOfRangeException("month", "Month must be between 1 and 12.");
			switch (month)
			{
				case 4:
				case 6:
				case 9:
				case 11:
					return 30;
				case 2:
					return IsLeapYear(year) ? 29 : 28;
				default:
					return 31;
			}
		}

		/// <summary>
		/// IsLeapYear for larger date range
		/// </summary>
		/// <param name="year"></param>
		/// <returns></returns>
		public static bool IsLeapYear(short year)
		{
			if (year == 0) throw new ArgumentOutOfRangeException("Year", "Year must not be 0");
			if (year == -32767) return true;
			if (year < 0) year = (short)(1 - year); // Adjustment for BC

			if (year % 4 == 0)
			{
				if (year % 100 == 0)
				{
					return (year % 400 == 0);
				}
				else
					return true;
			}
			else
				return false;
		}

	}
}
