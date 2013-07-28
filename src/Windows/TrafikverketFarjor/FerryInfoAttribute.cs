using System.Collections.Generic;

namespace TrafikverketFarjor
{
    public class FerryInfoAttribute
    {
        private readonly IList<IFerryInfoAttributeRule> _rules = new List<IFerryInfoAttributeRule>();
        public string Key { get; set; }
        public string Description { get; set; }
        public IList<IFerryInfoAttributeRule> Rules
        {
            get { return _rules; }
        }
    }
}