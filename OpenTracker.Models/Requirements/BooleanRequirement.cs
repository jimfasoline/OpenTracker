﻿using OpenTracker.Models.AccessibilityLevels;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This base class contains boolean requirement data.
    /// </summary>
    public abstract class BooleanRequirement : IRequirement
    {
        private readonly AccessibilityLevel _metAccessibility;

        public event EventHandler? ChangePropagated;
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _met;
        public bool Met
        {
            get => _met;
            private set
            {
                if (_met == value)
                {
                    return;
                }
                
                _met = value;
                OnPropertyChanged();
            }
        }

        private bool _testing;
        public bool Testing
        {
            get => _testing;
            set
            {
                if (_testing == value)
                {
                    return;
                }
                
                _testing = value;
                UpdateValue();
            }
        }

        public AccessibilityLevel Accessibility =>
            Met ? _metAccessibility : AccessibilityLevel.None;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="metAccessibility">
        /// The accessibility level when the condition is met.
        /// </param>
        public BooleanRequirement(
            AccessibilityLevel metAccessibility = AccessibilityLevel.Normal)
        {
            _metAccessibility = metAccessibility;
        }

        /// <summary>
        /// Raises the PropertyChanged and ChangePropagated events for all properties.
        /// </summary>
        private void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Met)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Accessibility)));
            ChangePropagated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Returns whether the condition is met.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the condition is met.
        /// </returns>
        protected abstract bool ConditionMet();

        /// <summary>
        /// Updates the value of the Met property.
        /// </summary>
        protected void UpdateValue()
        {
            Met = Testing || ConditionMet();
        }
    }
}
