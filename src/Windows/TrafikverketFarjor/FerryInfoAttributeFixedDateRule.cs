using System;

namespace TrafikverketFarjor
{
    public class FerryInfoAttributeFixedDateRule : IFerryInfoAttributeRule
    {
        private bool _exclude;
        private int _month;
        private int _day;

        public static FerryInfoAttributeFixedDateRule Exclude(int month, int day)
        {
            return new FerryInfoAttributeFixedDateRule { _month = month, _day = day, _exclude = true};
        }

        public static FerryInfoAttributeFixedDateRule Include(int month, int day)
        {
            return new FerryInfoAttributeFixedDateRule { _month = month, _day = day, _exclude = false };
        }

        public bool IsExcluded(DateTime dateTime)
        {
            return _exclude && dateTime.Month == _month && dateTime.Day == _day;
        }

        public bool IsIncluded(DateTime dateTime)
        {
            return !_exclude && dateTime.Month == _month && dateTime.Day == _day;
        }
    }
}