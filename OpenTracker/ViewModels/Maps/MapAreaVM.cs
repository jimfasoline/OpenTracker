﻿using Avalonia.Layout;
using Avalonia.Threading;
using OpenTracker.Models.Settings;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Maps.Connections;
using OpenTracker.ViewModels.Maps.Locations;
using ReactiveUI;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Maps
{
    /// <summary>
    /// This is the ViewModel of the map area control.
    /// </summary>
    public class MapAreaVM : ViewModelBase, IMapAreaVM
    {
        private readonly ILayoutSettings _layoutSettings;

        public Orientation Orientation => 
            _layoutSettings.CurrentMapOrientation;

        public List<IMapVM> Maps { get; }
        public IMapConnectionCollection Connectors { get; }
        public List<IMapLocationVMBase> MapLocations { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MapAreaVM(ILayoutSettings layoutSettings, IMapAreaVMFactory factory, IMapConnectionCollection connectors)
        {
            _layoutSettings = layoutSettings;

            Maps = factory.GetMapControlVMs();
            Connectors = connectors;
            MapLocations = factory.GetMapLocationControlVMs();

            _layoutSettings.PropertyChanged += OnLayoutChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the LayoutSettings class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnLayoutChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutSettings.CurrentMapOrientation))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Orientation)));
            }
        }
    }
}
