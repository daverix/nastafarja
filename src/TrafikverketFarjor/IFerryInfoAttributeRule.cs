using System;

namespace TrafikverketFarjor
{
    public interface IFerryInfoAttributeRule
    {
        bool IsExcluded(DateTime dateTime);
        bool IsIncluded(DateTime dateTime);
    }
}