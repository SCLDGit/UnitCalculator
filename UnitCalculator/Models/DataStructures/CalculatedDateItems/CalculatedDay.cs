using System;

namespace UnitCalculator.Models.DataStructures.CalculatedDateItems;

public class CalculatedDay : ICalculatedDateItem
{
    public DayOfWeek DayOfWeek { get; set; }
    public int Date { get; set; }
    public int Hours { get; set; }
    public int Units => Hours * 4;
}