using System;
using TrafikverketFarjor.DateTimeExtensions;

namespace TrafikverketFarjor
{
    public class FerryInfoAttributeDayOfWeekRule : IFerryInfoAttributeRule
    {
        private ScheduleDayOfWeek _exclude;
        private ScheduleDayOfWeek _include;

        public static FerryInfoAttributeDayOfWeekRule Exclude(ScheduleDayOfWeek dayOfWeek)
        {
            return new FerryInfoAttributeDayOfWeekRule {_exclude = dayOfWeek};
        }

        public static FerryInfoAttributeDayOfWeekRule Include(ScheduleDayOfWeek dayOfWeek)
        {
            return new FerryInfoAttributeDayOfWeekRule { _include = dayOfWeek };
        }

        public bool IsExcluded(DateTime dateTime)
        {
            return IsDefined(_exclude, dateTime);
        }

        public bool IsIncluded(DateTime dateTime)
        {
            return IsDefined(_include, dateTime);
        }

        private static bool IsDefined(ScheduleDayOfWeek haystack, DateTime dateTime)
        {
            if ((haystack & ScheduleDayOfWeek.Holiday) != 0 && DateTimeUtil.IsHoliday(dateTime))
                return true;

            if ((haystack & dateTime.DayOfWeek.ToScheduleDayOfWeek()) != 0)
                return true;

            return false;
        }
    }
}