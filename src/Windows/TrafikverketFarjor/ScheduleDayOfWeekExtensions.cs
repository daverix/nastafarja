using System;
using System.Collections.Generic;
using System.Linq;

namespace TrafikverketFarjor
{
    public static class ScheduleDayOfWeekExtensions
    {
        public static IEnumerable<DayOfWeek> ToDayOfWeek(this ScheduleDayOfWeek value)
        {
            return Enum.GetValues(typeof (DayOfWeek))
                       .Cast<DayOfWeek>()
                       .Where(dayOfWeek => (value & ToScheduleDayOfWeek(dayOfWeek)) != 0);
        }

        public static ScheduleDayOfWeek ToScheduleDayOfWeek(this DayOfWeek value)
        {
            switch (value)
            {
                case DayOfWeek.Monday: return ScheduleDayOfWeek.Monday;
                case DayOfWeek.Tuesday: return ScheduleDayOfWeek.Tuesday;
                case DayOfWeek.Wednesday: return ScheduleDayOfWeek.Wednesday;
                case DayOfWeek.Thursday: return ScheduleDayOfWeek.Thursday;
                case DayOfWeek.Friday: return ScheduleDayOfWeek.Friday;
                case DayOfWeek.Saturday: return ScheduleDayOfWeek.Saturday;
                case DayOfWeek.Sunday: return ScheduleDayOfWeek.Sunday;
            }

            throw new ArgumentOutOfRangeException(string.Format("Could not convert {0} to ScheduleDayOfWeek", value));
        }
    }
}