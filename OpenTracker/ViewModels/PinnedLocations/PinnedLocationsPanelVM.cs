﻿using Avalonia.Layout;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using Avalonia.Threading;

namespace OpenTracker.ViewModels.PinnedLocations
{
    /// <summary>
    /// This is the ViewModel for the Locations panel control.
    /// </summary>
    public class PinnedLocationsPanelVM : ViewModelBase, IPinnedLocationsPanelVM
    {
        private readonly ILayoutSettings _layoutSettings;

        public Orientation Orientation =>
            _layoutSettings.CurrentLayoutOrientation;

        public IPinnedLocationVMCollection Locations { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PinnedLocationsPanelVM(
            ILayoutSettings layoutSettings, IPinnedLocationVMCollection locations)
        {
            _layoutSettings = layoutSettings;

            Locations = locations;

            _layoutSettings.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ILayoutSettings interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.CurrentLayoutOrientation))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Orientation)));
            }
        }
    }
}
