using System;

namespace UnitCalculator.Models.DataStructures;

public class CalculationParameters
{
    public DateTime StartDate    { get; set; }
    public DateTime EndDate      { get; set; }
    public int      MondayHours  { get; set; }
    public int      TuesdayHours { get; set; }
    public int      WednesdayHours { get; set; }
    public int      ThursdayHours { get; set; }
    public int      FridayHours  { get; set; }
    public int      SaturdayHours { get; set; }
    public int      SundayHours  { get; set; }
}