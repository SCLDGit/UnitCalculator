using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Helpers;

namespace UnitCalculator.ViewModels
{
    public abstract class ViewModelBase : ReactiveValidationObject, IReactiveObject
    {
    }
}