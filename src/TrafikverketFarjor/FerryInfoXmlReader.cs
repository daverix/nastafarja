using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace TrafikverketFarjor
{
    public class FerryInfoXmlReader
    {
        public FerryInfo Read(Stream stream)
        {
            var xdoc = XDocument.Load(stream);
            var result = new FerryInfo();
            var xinfo = GetExpectedElement(xdoc, "FerryInfo");
            result.Name = GetExpectedElement(xinfo, "Name").Value;
            result.Region = xinfo.Elements("Region").Select(x => x.Value).FirstOrDefault();
            result.Url = xinfo.Elements("Url").Select(x => x.Value).FirstOrDefault();
            var xroutes = GetExpectedElement(xinfo, "Routes");
            foreach (var xroute in xroutes.Elements("Route"))
            {
                result.Routes.Add(ReadRoute(result, xroute));
            }

            LoadAttributes(xinfo, result);

            foreach (var info in xinfo.Descendants("div"))
            {
                var headline = info.Element("h1").Value;
                var body = info.Element("p").Value;
                result.Info.Add(headline, body);
            }
            return result;
        }

        private void LoadAttributes(XContainer xinfo, FerryInfo result)
        {
            foreach (var xattribute in xinfo.Descendants("Attribute"))
            {
                var attribute = ReadAttribute(xattribute);
                result.Attributes.Add(attribute);
            }
        }

        private FerryInfoAttribute ReadAttribute(XContainer xattribute)
        {
            var attribute = new FerryInfoAttribute
                {
                    Key = GetExpectedElement(xattribute, "Key").Value,
                    Description = GetExpectedElement(xattribute, "Description").Value
                };

            foreach (var xexclude in xattribute.Descendants("Exclude"))
            {
                var rule = ReadExcludeTag(xexclude);
                attribute.Rules.Add(rule);
            }

            return attribute;
        }

        private IFerryInfoAttributeRule ReadExcludeTag(XContainer xexclude)
        {
            var what = GetExpectedElement(xexclude, "What").Value;
            var when = GetExpectedElement(xexclude, "When").Value;

            if (what == "FixedDate")
            {
                var parts = when.Split(' ');
                if (parts.Length != 2) throw new FormatException("When for FixedDate must be formatted as <DD mmmm>: " + when);
                var day = Convert.ToInt32(parts[0], CultureInfo.InvariantCulture);
                var month = GetMonthFromName(parts[1]);

                return FerryInfoAttributeFixedDateRule.Exclude(month, day);
            }

            if (what == "DayOfWeek")
            {
                var dayOfWeek = (ScheduleDayOfWeek) Enum.Parse(typeof (ScheduleDayOfWeek), when);
                return FerryInfoAttributeDayOfWeekRule.Exclude(dayOfWeek);
            }

            throw new FormatException(string.Format("Rule What is not supported: " + what));
        }

        private int GetMonthFromName(string nameOfMonth)
        {
            for (var i = 1; i <= 12; i++)
            {
                var expectedNameOfMonth = new DateTime(2013, i, 1).ToString("MMMM", CultureInfo.InvariantCulture);
                if (expectedNameOfMonth.Equals(nameOfMonth, StringComparison.InvariantCultureIgnoreCase))
                    return i;
            }

            throw new ArgumentOutOfRangeException("nameOfMonth", nameOfMonth, "Name of month is not recognized as a month.");
        }

        private FerryRoute ReadRoute(FerryInfo ferryInfo, XContainer xroute)
        {
            var result = new FerryRoute(ferryInfo);
            result.DepartsFrom = GetExpectedElement(xroute, "DepartsFrom").Value;
            result.ArrivesAt = GetExpectedElement(xroute, "ArrivesAt").Value;
            var xtitle = xroute.Element("Title");
            result.Title = xtitle != null
                ? xtitle.Value
                : string.Format("{0}-{1}", result.DepartsFrom, result.ArrivesAt);

            foreach (var xschedule in GetExpectedElement(xroute, "Schedules").Elements("Schedule"))
            {
                LoadSchedule(xschedule, result);
            }

            return result;
        }

        private void LoadSchedule(XElement xschedule, FerryRoute route)
        {
            var dayOfWeek = (ScheduleDayOfWeek)Enum.Parse(typeof(ScheduleDayOfWeek), GetExpectedElement(xschedule, "DayOfWeek").Value);
            foreach (var xentry in xschedule.Elements("Entry"))
            {
                var hour = Convert.ToInt32(xentry.Attribute("hour").Value, CultureInfo.InvariantCulture);
                var values = xentry.Attribute("values").Value.Split(' ');
                foreach (var value in values.Where(s => !string.IsNullOrWhiteSpace(s)))
                {
                    var match = Regex.Match(value, "^([0-9]+)(.)?$");
                    if (!match.Success)
                    {
                        throw new FormatException("Unrecognized entry value pattern: "+ value);
                    }

                    var minute = Convert.ToInt32(match.Groups[1].Value, CultureInfo.InvariantCulture);
                    var attribute = match.Groups[2].Success 
                        ? match.Groups[2].Value
                        : null;

                    var schedule = new FerrySchedule
                        {
                            Attribute = attribute,
                            DayOfWeek = dayOfWeek,
                            Departs = new TimeSpan(hour, minute, 00)
                        };

                    route.AddSchedule(schedule);
                }
            }
        }

        private XElement GetExpectedElement(XContainer node, XName name)
        {
            var element = node.Element(name);
            if (element == null) throw new FormatException(string.Format("Expected element {0}", name));
            return element;
        }
    }
}
