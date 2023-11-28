using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using UnitCalculator.Models.DataStructures.CalculatedDateItems;
using ReactiveUI;

namespace UnitCalculator.Views.Controls;

public partial class CalendarControl : UserControl
{
    public CalendarControl()
    {
        InitializeComponent();

        this.WhenAnyValue(p_control => p_control.ShowUnits,
                          p_control => p_control.SelectedMonth)
            .Subscribe(p_tuple =>
                       {
                           UpdateMonthLayout(p_tuple.Item2);
                       });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void UpdateMonthLayout(CalculatedMonth? p_month)
    {
        if (p_month is null) return;
        
        var calendarGrid = this.FindControl<Grid>("CalendarGrid");

        calendarGrid!.Children.RemoveRange(8, calendarGrid.Children.Count - 8 );
        
        var counter = 1;

        var daysCounter = 1;
        
        var startDate = new DateTime(p_month.Year, (int) p_month.Month, 1);

        var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);

        var firstDateIndex = (int) startDate.DayOfWeek;

        for (var i = 2; i < 8; ++i)
        {
            for (var j = 0; j < 7; ++j)
            {
                if (counter < firstDateIndex || daysCounter > daysInMonth)
                {
                    var border = new Border
                                 {
                                     Background = Brushes.LightGray,
                                     BorderThickness = new Thickness(1),
                                     BorderBrush = Brushes.DimGray,
                                     HorizontalAlignment = HorizontalAlignment.Stretch,
                                     VerticalAlignment = VerticalAlignment.Stretch,
                                     MinHeight = 38
                                 };

                    var childGrid = new Grid();
                    
                    border.Child = childGrid;
                    
                    calendarGrid.Children.Add(border);
                    
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);

                    counter++;
                }
                else
                {
                    var border = new Border
                                 {
                                     BorderThickness     = new Thickness(1),
                                     BorderBrush         = Brushes.DimGray,
                                     HorizontalAlignment = HorizontalAlignment.Stretch,
                                     VerticalAlignment   = VerticalAlignment.Stretch,
                                     MinHeight           = 38
                                 };

                    var childGrid = new Grid
                                    {
                                        RowDefinitions = new RowDefinitions
                                                         {
                                                             new (GridLength.Auto),
                                                             new (GridLength.Auto)
                                                         }
                                    };

                    var dayLabel = new TextBlock
                                   {
                                       HorizontalAlignment = HorizontalAlignment.Left,
                                       VerticalAlignment = VerticalAlignment.Top,
                                       Margin = new Thickness(2),
                                       Text     = daysCounter.ToString(),
                                       FontSize = 8
                                   };
                    
                    childGrid.Children.Add(dayLabel);
                    
                    Grid.SetRow(dayLabel, 0);
                    
                    var matchingDay = p_month.Days.FirstOrDefault(p_day => p_day.Date == daysCounter && p_day.Hours > 0);

                    if (matchingDay is not null)
                    {
                        var hoursLabel = new TextBlock
                                         {
                                             HorizontalAlignment = HorizontalAlignment.Left,
                                             VerticalAlignment = VerticalAlignment.Top,
                                             Margin = new Thickness(2),
                                             Text     = ShowUnits ? matchingDay.Units.ToString() : matchingDay.Hours.ToString(),
                                             FontSize = 14
                                         };
                        
                        childGrid.Children.Add(hoursLabel);
                        
                        Grid.SetRow(hoursLabel, 1);

                        border.Background = Brushes.PaleGreen;
                    }
                    else
                    {
                        border.Background = Brushes.LightPink;
                    }
                    
                    border.Child = childGrid;
                    
                    calendarGrid.Children.Add(border);
                    
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    
                    counter++;
                    daysCounter++;
                }
            }
        }
    }
    
    public static readonly StyledProperty<CalculatedMonth> SelectedMonthProperty =
        AvaloniaProperty.Register<CalendarControl, CalculatedMonth>(nameof(SelectedMonth));
    
    public CalculatedMonth SelectedMonth
    {
        get => GetValue(SelectedMonthProperty);
        set => SetValue(SelectedMonthProperty, value);
    }
    
    public static readonly StyledProperty<bool> ShowUnitsProperty =
        AvaloniaProperty.Register<CalendarControl, bool>(nameof(ShowUnits));
    
    public bool ShowUnits
    {
        get => GetValue(ShowUnitsProperty);
        set => SetValue(ShowUnitsProperty, value);
    }
}