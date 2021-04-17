﻿using System.ComponentModel;
using OpenTracker.Models.Requirements;
using ReactiveUI;

namespace OpenTracker.Models.Locations.Map
{
    /// <summary>
    ///     This class contains map location data.
    /// </summary>
    public class MapLocation : ReactiveObject, IMapLocation
    {
        private readonly IRequirement? _requirement;

        public ILocation Location { get; }
        public MapID Map { get; }
        public double X { get; }
        public double Y { get; }

        public bool RequirementMet => _requirement is null || _requirement.Met;
        public bool Visible => Location.Visible;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="map">
        ///     The map identity on which the location is represented.
        /// </param>
        /// <param name="x">
        ///     The X coordinate of the map location.
        /// </param>
        /// <param name="y">
        ///     The Y coordinate of the map location.
        /// </param>
        /// <param name="location">
        ///     The location parent class.
        /// </param>
        /// <param name="requirement">
        ///     The mode requirement for displaying this map location.
        /// </param>
        public MapLocation(MapID map, double x, double y, ILocation location, IRequirement? requirement = null)
        {
            Map = map;
            X = x;
            Y = y;
            Location = location;
            _requirement = requirement;

            Location.PropertyChanged += OnLocationChanged;
            
            if (_requirement is not null)
            {
                _requirement.PropertyChanged += OnRequirementChanged;
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the ILocation interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnLocationChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILocation.Visible))
            {
                this.RaisePropertyChanged(nameof(Visible));
            }
        }

        /// <summary>
        ///     Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        ///     The sending object of the event.
        /// </param>
        /// <param name="e">
        ///     The arguments of the PropertyChanged event.
        /// </param>
        private void OnRequirementChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Met))
            {
                this.RaisePropertyChanged(nameof(RequirementMet));
            }
        }
    }
}