﻿using OpenTracker.Models.AccessibilityLevels;
using System;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This is the interface for requirements.
    /// </summary>
    public interface IRequirement : INotifyPropertyChanged
    {
        bool Met { get; }
        AccessibilityLevel Accessibility { get; }
        bool Testing { get; set; }

        event EventHandler? ChangePropagated;
    }
}
