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

		void validateDate()
		{
			if (month > 12) throw new ArgumentOutOfRangeException("Month");
			if (day > 31) throw new ArgumentOutOfRangeException("Day");

			if (month > 0 && day > 0)
			{
				int maxDay = DateTime.DaysInMonth(Year ?? 2000, month);
				if (day > maxDay) throw new ArgumentOutOfRangeException("Day");
			}
		}

	}
}
