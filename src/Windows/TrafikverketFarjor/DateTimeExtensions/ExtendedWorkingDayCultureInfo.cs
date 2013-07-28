using System;
using System.Collections.Generic;
using System.Linq;
using DateTimeExtensions.WorkingDays;

namespace TrafikverketFarjor.DateTimeExtensions
{
    public class ExtendedWorkingDayCultureInfo : WorkingDayCultureInfo
    {
        public ExtendedWorkingDayCultureInfo()
        {
            InitializeStrategies();
        }

        public ExtendedWorkingDayCultureInfo(string name) : base(name)
        {
            InitializeStrategies();
        }

        public ExtendedWorkingDayCultureInfo(IHolidayStrategy strategy)
        {
            LocateHolidayStrategies.Insert(0, name =>
                {
                    return strategy;
                });

            InitializeStrategies();
        }

        private void InitializeStrategies()
        {
            _locateHolidayStrategies.Add(DefaultLocateHolidayStrategy);
            _locateWorkingDayOfWeekStrategies.Add(DefaultLocateWorkingDayOfWeekStrategy);

            LocateHolidayStrategy = name => _locateHolidayStrategies
                                                .Select(s => s(name))
                                                .FirstOrDefault(s => s != null) ?? DefaultLocateHolidayStrategy(name);
            LocateWorkingDayOfWeekStrategy = name => _locateWorkingDayOfWeekStrategies
                                                         .Select(s => s(name))
                                                         .FirstOrDefault(s => s != null) ??
                                                     DefaultLocateWorkingDayOfWeekStrategy(name);
        }

        public IList<Func<string, IHolidayStrategy>> LocateHolidayStrategies
        {
            get { return _locateHolidayStrategies; }
        }

        public IList<Func<string, IWorkingDayOfWeekStrategy>> LocateWorkingDayOfWeekStrategies
        {
            get { return _locateWorkingDayOfWeekStrategies; }
        }

        private readonly IList<Func<string, IHolidayStrategy>> _locateHolidayStrategies = new List<Func<string, IHolidayStrategy>>();
        private readonly IList<Func<string, IWorkingDayOfWeekStrategy>> _locateWorkingDayOfWeekStrategies = new List<Func<string, IWorkingDayOfWeekStrategy>>(); 
    }
}