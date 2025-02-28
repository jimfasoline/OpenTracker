﻿using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains map shuffle setting requirement data.
    /// </summary>
    public class MapShuffleRequirement : BooleanRequirement
    {
        private readonly IMode _mode;
        private readonly bool _expectedValue;

        public delegate MapShuffleRequirement Factory(bool expectedValue);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="expectedValue">
        /// A boolean expected map shuffle value.
        /// </param>
        public MapShuffleRequirement(IMode mode, bool expectedValue)
        {
            _mode = mode;
            _expectedValue = expectedValue;

            _mode.PropertyChanged += OnModeChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.MapShuffle))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _mode.MapShuffle == _expectedValue;
        }
    }
}
