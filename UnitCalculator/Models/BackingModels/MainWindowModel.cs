using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using UnitCalculator.Models.DataStructures;
using UnitCalculator.Models.DataStructures.CalculatedDateItems;
using UnitCalculator.Models.Enumerations;
using UnitCalculator.Views.Controls;

namespace UnitCalculator.Models.BackingModels;

public class MainWindowModel
{
    public async Task<IEnumerable<CalculatedMonth>> GetCalculatedMonths(CalculationParameters p_parameters)
    {
        var calculatedMonths = new List<CalculatedMonth>();

        var currentDateTime = p_parameters.StartDate;

        var monthHasBeenProcessed = false;

        var currentMonth = 0;
        
        while (currentDateTime <= p_parameters.EndDate)
        {
            if (!monthHasBeenProcessed)
            {
                currentMonth = currentDateTime.Month;
                calculatedMonths.Add(GetCalculatedMonth(currentDateTime.Month, currentDateTime.Year, currentDateTime.Day, p_parameters));
                monthHasBeenProcessed = true;
            }

            currentDateTime = currentDateTime.AddDays(1);

            if (currentDateTime.Month != currentMonth)
            {
                monthHasBeenProcessed = false;
            }
        }

        return await Task.FromResult(calculatedMonths);
    }

    private static int GetTotalHoursForMonth(IReadOnlyDictionary<DayOfWeek, int> p_daysOfWeekCount, int p_mondayHours,
                                             int p_tuesdayHours, int p_wednesdayHours, int p_thursdayHours,
                                             int p_fridayHours, int p_saturdayHours, int p_sundayHours)
    {
        var totalHours = 0;

        totalHours += p_daysOfWeekCount[DayOfWeek.Monday]    * p_mondayHours;
        totalHours += p_daysOfWeekCount[DayOfWeek.Tuesday]   * p_tuesdayHours;
        totalHours += p_daysOfWeekCount[DayOfWeek.Wednesday] * p_wednesdayHours;
        totalHours += p_daysOfWeekCount[DayOfWeek.Thursday]  * p_thursdayHours;
        totalHours += p_daysOfWeekCount[DayOfWeek.Friday]    * p_fridayHours;
        totalHours += p_daysOfWeekCount[DayOfWeek.Saturday]  * p_saturdayHours;
        totalHours += p_daysOfWeekCount[DayOfWeek.Sunday]    * p_sundayHours;

        return totalHours;
    }

    private static CalculatedMonth GetCalculatedMonth(int p_month, int p_year, int p_startingDate, CalculationParameters p_parameters)
    {
        var month = new CalculatedMonth
                    {
                        Month = (Month)p_month,
                        Year  = p_year
                    };

        var finalDateInMonth = p_parameters.EndDate.Month == p_month && p_parameters.EndDate.Year == p_year ? p_parameters.EndDate.Day : DateTime.DaysInMonth(p_year, p_month);
        
        for (var day = p_startingDate; day <= finalDateInMonth; day++)
        {
            var date = new DateTime(p_year, p_month, day);

            var calculatedDay = new CalculatedDay
                                {
                                    DayOfWeek = date.DayOfWeek,
                                    Date      = day,
                                    Hours = date.DayOfWeek switch
                                            {
                                                DayOfWeek.Monday => p_parameters.MondayHours,
                                                DayOfWeek.Tuesday => p_parameters.TuesdayHours,
                                                DayOfWeek.Wednesday => p_parameters.WednesdayHours,
                                                DayOfWeek.Thursday => p_parameters.ThursdayHours,
                                                DayOfWeek.Friday => p_parameters.FridayHours,
                                                DayOfWeek.Saturday => p_parameters.SaturdayHours,
                                                DayOfWeek.Sunday => p_parameters.SundayHours,
                                                _ => throw new ArgumentOutOfRangeException()
                                            }
                                };

            month.Days.Add(calculatedDay);
        }
        
        return month;
    }

    // A method that returns a dictionary with the number of days of each day of the week in a given month
    private static Dictionary<DayOfWeek, int> GetDaysOfWeekCountForMonth(int p_month, int p_year, int p_startingDate)
    {
        var daysOfWeekCountDictionary = new Dictionary<DayOfWeek, int>();

        var mondayCount    = 0;
        var tuesdayCount   = 0;
        var wednesdayCount = 0;
        var thursdayCount  = 0;
        var fridayCount    = 0;
        var saturdayCount  = 0;
        var sundayCount    = 0;

        var daysInMonth = DateTime.DaysInMonth(p_year, p_month);

        for (var day = p_startingDate; day <= daysInMonth; day++)
        {
            var date = new DateTime(p_year, p_month, day);

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    mondayCount++;
                    break;
                case DayOfWeek.Tuesday:
                    tuesdayCount++;
                    break;
                case DayOfWeek.Wednesday:
                    wednesdayCount++;
                    break;
                case DayOfWeek.Thursday:
                    thursdayCount++;
                    break;
                case DayOfWeek.Friday:
                    fridayCount++;
                    break;
                case DayOfWeek.Saturday:
                    saturdayCount++;
                    break;
                case DayOfWeek.Sunday:
                    sundayCount++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        daysOfWeekCountDictionary.Add(DayOfWeek.Monday, mondayCount);
        daysOfWeekCountDictionary.Add(DayOfWeek.Tuesday, tuesdayCount);
        daysOfWeekCountDictionary.Add(DayOfWeek.Wednesday, wednesdayCount);
        daysOfWeekCountDictionary.Add(DayOfWeek.Thursday, thursdayCount);
        daysOfWeekCountDictionary.Add(DayOfWeek.Friday, fridayCount);
        daysOfWeekCountDictionary.Add(DayOfWeek.Saturday, saturdayCount);
        daysOfWeekCountDictionary.Add(DayOfWeek.Sunday, sundayCount);

        return daysOfWeekCountDictionary;
    } 
    
    public async Task ClickAboutButton(Window p_window)
    {
        await AboutPanel.Show(p_window);
    }
}