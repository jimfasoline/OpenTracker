﻿using OpenTracker.Models.AccessibilityLevels;
using System.Collections.Generic;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This interface contains dungeon accessibility result data.
    /// </summary>
    public interface IDungeonResult
    {
        delegate IDungeonResult Factory(
            List<AccessibilityLevel> bossAccessibility, AccessibilityLevel accessibility,
            int accessible);

        AccessibilityLevel Accessibility { get; }
        int Accessible { get; }
        List<AccessibilityLevel> BossAccessibility { get; }
    }
}