using System;

namespace TrafikverketFarjor
{
    [Flags]
    public enum ScheduleDayOfWeek
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64,
        Holiday = 128,

        Weekdays = Monday|Tuesday|Wednesday|Thursday|Friday,
        Weekends = Saturday|Sunday|Holiday,
        Everyday = Weekdays|Weekends,
    }
}