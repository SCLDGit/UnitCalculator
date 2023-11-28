using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using UnitCalculator.Models.DataStructures.CalculatedDateItems;

namespace UnitCalculator.Views.Converters;

public class UnitConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> p_values, Type p_targetType, object? p_parameter, CultureInfo p_culture)
    {
        if (p_values[0] is not bool boolValue || p_values[1] is not CalculatedMonth month) return string.Empty;

        return boolValue switch
               {
                   true  => month.Units.ToString(),
                   false => month.Hours.ToString()
               };
    }
}