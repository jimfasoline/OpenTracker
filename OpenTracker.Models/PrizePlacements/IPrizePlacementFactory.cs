﻿namespace OpenTracker.Models.PrizePlacements
{
    public interface IPrizePlacementFactory
    {
        IPrizePlacement GetPrizePlacement(PrizePlacementID id);
    }
}