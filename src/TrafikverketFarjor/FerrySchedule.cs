using System;

namespace TrafikverketFarjor
{
    public class FerrySchedule
    {
        public TimeSpan Departs { get; set; }
        public ScheduleDayOfWeek DayOfWeek { get; set; }
        public string Attribute { get; set; }
    }
}