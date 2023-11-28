using System.Linq;
using Avalonia.Collections;
using UnitCalculator.Models.Enumerations;

namespace UnitCalculator.Models.DataStructures.CalculatedDateItems;

public class CalculatedMonth : ICalculatedDateItem
{
    public Month                       Month { get; set; }
    public int                         Year  { get; set; }
    public AvaloniaList<CalculatedDay> Days  { get; } = new();
    public int                         Hours => Days.Sum(p_day => p_day.Hours);
    public int                         Units => Hours * 4;
}