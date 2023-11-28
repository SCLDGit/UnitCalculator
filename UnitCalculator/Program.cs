using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;

namespace UnitCalculator
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] p_args)
        {
            // var daysOfWeekCount = GetDaysOfWeekCountForMonth(2, 2023, 1);
            //
            // var totalHoursForMonth = GetTotalHoursForMonth(daysOfWeekCount, 9, 9, 9, 9, 9, 24, 24);
            // var totalUnits         = totalHoursForMonth * 4;
            BuildAvaloniaApp()
               .UseReactiveUI()
               .StartWithClassicDesktopLifetime(p_args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                         .UsePlatformDetect()
                         .LogToTrace()
                         .UseReactiveUI();
    }
}