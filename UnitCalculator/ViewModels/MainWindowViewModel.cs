using System;
using System.Threading.Tasks;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Metadata;
using UnitCalculator.Models.BackingModels;
using UnitCalculator.Models.DataStructures;
using UnitCalculator.Models.DataStructures.CalculatedDateItems;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;

namespace UnitCalculator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Model = new MainWindowModel();

            var dateOrderObservable = this.WhenAny(p_vm => p_vm.StartDate,
                         p_vm => p_vm.EndDate,
                         (p_startDate, p_endDate) => p_startDate.Value < p_endDate.Value);
            
            var dateLengthObservable = this.WhenAny(p_vm => p_vm.StartDate,
                         p_vm => p_vm.EndDate,
                         (p_startDate, p_endDate) => p_endDate.Value.Subtract(p_startDate.Value).Days <= 364);

            
            this.ValidationRule(p_vm => p_vm.EndDate,
                                dateOrderObservable,
                                "Service end date must be after the service start date");
            
            this.ValidationRule(p_vm => p_vm.EndDate,
                                dateLengthObservable,
                                "Service end date cannot be more than 364 days after the service start date");

            this.ValidationRule(p_vm => p_vm.MondayHours,
                                p_hours => p_hours >= 0,
                                "Hours must be greater than or equal to 0");
            
            this.ValidationRule(p_vm => p_vm.MondayHours,
                                p_hours => p_hours <= 24,
                                "Hours must be less than or equal to 24");
            
            this.ValidationRule(p_vm => p_vm.TuesdayHours,
                                p_hours => p_hours >= 0,
                                "Hours must be greater than or equal to 0");
            
            this.ValidationRule(p_vm => p_vm.TuesdayHours,
                                p_hours => p_hours <= 24,
                                "Hours must be less than or equal to 24");
            
            this.ValidationRule(p_vm => p_vm.WednesdayHours,
                                p_hours => p_hours >= 0,
                                "Hours must be greater than or equal to 0");
            
            this.ValidationRule(p_vm => p_vm.WednesdayHours,
                                p_hours => p_hours <= 24,
                                "Hours must be less than or equal to 24");
            
            this.ValidationRule(p_vm => p_vm.ThursdayHours,
                                p_hours => p_hours >= 0,
                                "Hours must be greater than or equal to 0");
            
            this.ValidationRule(p_vm => p_vm.ThursdayHours,
                                p_hours => p_hours <= 24,
                                "Hours must be less than or equal to 24");
            
            this.ValidationRule(p_vm => p_vm.FridayHours,
                                p_hours => p_hours >= 0,
                                "Hours must be greater than or equal to 0");
            
            this.ValidationRule(p_vm => p_vm.FridayHours,
                                p_hours => p_hours <= 24,
                                "Hours must be less than or equal to 24");
            
            this.ValidationRule(p_vm => p_vm.SaturdayHours,
                                p_hours => p_hours >= 0,
                                "Hours must be greater than or equal to 0");
            
            this.ValidationRule(p_vm => p_vm.SaturdayHours,
                                p_hours => p_hours <= 24,
                                "Hours must be less than or equal to 24");
            
            this.ValidationRule(p_vm => p_vm.SundayHours,
                                p_hours => p_hours >= 0,
                                "Hours must be greater than or equal to 0");
            
            this.ValidationRule(p_vm => p_vm.SundayHours,
                                p_hours => p_hours <= 24,
                                "Hours must be less than or equal to 24");
        }

        private MainWindowModel Model { get; init; }

        
        [DependsOn(nameof(EndDate))]
        [Reactive] public DateTime StartDate { get; set; } = DateTime.Today;
        
        [DependsOn(nameof(StartDate))]
        [Reactive] public DateTime EndDate   { get; set; } = DateTime.Today.AddYears(1).Subtract(TimeSpan.FromDays(1));

        [Reactive] public int                 MondayHours               { get; set; }
        [Reactive] public int                 TuesdayHours              { get; set; }
        [Reactive] public int                 WednesdayHours            { get; set; }
        [Reactive] public int                 ThursdayHours             { get; set; }
        [Reactive] public int                 FridayHours               { get; set; }
        [Reactive] public int                 SaturdayHours             { get; set; }
        [Reactive] public int                 SundayHours               { get; set; }
        [Reactive] public bool                ResultsHaveBeenCalculated { get; set; }
        [Reactive] public bool                ToggleIsEnabled           { get; set; }
        AvaloniaList<CalculatedMonth>         Months                    { get; } = new();
        [Reactive] public CalculatedMonth?    SelectedMonth             { get; set; }
        
        // public bool CanClickCalculateUnits(object p_parameter)
        // {
        //     return ValidationContext.GetIsValid();
        // }
        
        public async Task ClickCalculateUnits(object p_parameter)
        {
            SelectedMonth = null;
            
            Months.Clear();

            Months.AddRange(await Model.GetCalculatedMonths(new CalculationParameters
                                                            {
                                                                StartDate      = StartDate,
                                                                EndDate        = EndDate,
                                                                MondayHours    = MondayHours,
                                                                TuesdayHours   = TuesdayHours,
                                                                WednesdayHours = WednesdayHours,
                                                                ThursdayHours  = ThursdayHours,
                                                                FridayHours    = FridayHours,
                                                                SaturdayHours  = SaturdayHours,
                                                                SundayHours    = SundayHours
                                                            }));
            ResultsHaveBeenCalculated = true;
        }

        public async Task ClickAboutButton(object p_parameter)
        {
            if ( p_parameter is not Window window ) return;
            await Model.ClickAboutButton(window);
        }
    }
}