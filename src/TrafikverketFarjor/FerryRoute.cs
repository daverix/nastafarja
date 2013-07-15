using System;
using System.Collections.Generic;
using System.Linq;
using TrafikverketFarjor.DateTimeExtensions;

namespace TrafikverketFarjor
{
    public class FerryRoute
    {
        private readonly FerryInfo _ferryInfo;
        private readonly List<FerrySchedule> _schedules = new List<FerrySchedule>();

        public FerryRoute(FerryInfo ferryInfo)
        {
            if (ferryInfo == null) throw new ArgumentNullException("ferryInfo");
            _ferryInfo = ferryInfo;
        }

        public string Title { get; set; }
        public string DepartsFrom { get; set; }
        public string ArrivesAt { get; set; }

        public void AddSchedule(FerrySchedule schedule)
        {
            if (schedule == null) throw new ArgumentNullException("schedule");
            _schedules.Add(schedule);
        }

        public IEnumerable<FerrySchedule> GetSchedule(DateTime dateTime)
        {
            return _schedules
                .Where(s => (s.DayOfWeek & GetDayOfWeek(dateTime)) != 0)
                .Where(s => !_ferryInfo.Attributes.Any(a => a.Key == s.Attribute && a.Rules.Any(r => r.IsExcluded(dateTime))))
                .OrderBy(s => s.Departs);
        }

        public FerrySchedule NextDeparture(DateTime dateTime)
        {
            return GetSchedule(dateTime)
                .FirstOrDefault(s => s.Departs >= dateTime.TimeOfDay);
        }

        private static ScheduleDayOfWeek GetDayOfWeek(DateTime dateTime)
        {
            return DateTimeUtil.IsHoliday(dateTime)
                ? ScheduleDayOfWeek.Holiday
                : dateTime.DayOfWeek.ToScheduleDayOfWeek();
        }
    }
}