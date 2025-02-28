﻿using OpenTracker.Models.AccessibilityLevels;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This base class contains non-boolean requirement data.
    /// </summary>
    public abstract class AccessibilityRequirement : IRequirement
    {
        public event EventHandler? ChangePropagated;
        public event PropertyChangedEventHandler? PropertyChanged;

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged();
                }
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

        public bool Met => 
            Accessibility > AccessibilityLevel.None;

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
        /// Returns the current accessibility of the requirement.
        /// </summary>
        /// <returns>
        /// The accessibility level.
        /// </returns>
        protected abstract AccessibilityLevel GetAccessibility();

        /// <summary>
        /// Updates the value of the Accessibility property.
        /// </summary>
        protected void UpdateValue()
        {
            Accessibility = Testing ? AccessibilityLevel.Normal : GetAccessibility();
        }
    }
}
