﻿using ReactiveUI;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This interface contains the top menu control ViewModel data.
    /// </summary>
    public interface ITopMenuVM
    {
        ReactiveCommand<Unit, Unit> Reset { get; }
        ReactiveCommand<Unit, Unit> Undo { get; }
        ReactiveCommand<Unit, Unit> Redo { get; }
        ReactiveCommand<Unit, Unit> ToggleDisplayAllLocations { get; }
        ReactiveCommand<Unit, Unit> Open { get; }
        ReactiveCommand<Unit, Unit> Save { get; }
        ReactiveCommand<Unit, Unit> SaveAs { get; }
    }
}
