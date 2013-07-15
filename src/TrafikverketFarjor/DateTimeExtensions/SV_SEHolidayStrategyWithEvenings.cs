using DateTimeExtensions.WorkingDays;
using DateTimeExtensions.WorkingDays.CultureStrategies;

namespace TrafikverketFarjor.DateTimeExtensions
{
    public class SV_SEHolidayStrategyWithEvenings : SV_SEHolidayStrategy
    {
        public SV_SEHolidayStrategyWithEvenings()
        {
            InnerHolidays.Add(GlobalHolidays.MidsummerEve);
            InnerHolidays.Add(ChristianHolidays.ChristmasEve);
            InnerHolidays.Add(GlobalHolidays.NewYearsEve);
        }
    }
}