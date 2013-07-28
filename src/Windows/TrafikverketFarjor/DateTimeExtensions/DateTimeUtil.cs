using System;
using DateTimeExtensions;

namespace TrafikverketFarjor.DateTimeExtensions
{
    public static class DateTimeUtil
    {
        public static bool IsHoliday(DateTime dateTime)
        {
            var workingDayCultureInfo = new ExtendedWorkingDayCultureInfo
                (new SV_SEHolidayStrategyWithEvenings());
            return dateTime.IsHoliday(workingDayCultureInfo);
        }
    }
}