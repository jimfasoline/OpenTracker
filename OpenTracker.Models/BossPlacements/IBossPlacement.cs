﻿using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.BossPlacements
{
    /// <summary>
    /// This is the interface for a boss placement.
    /// </summary>
    public interface IBossPlacement : INotifyPropertyChanged, ISaveable<BossPlacementSaveData>
    {
        BossType? Boss { get; set; }
        BossType DefaultBoss { get; }

        delegate IBossPlacement Factory(BossType defaultBoss);

        BossType? GetCurrentBoss();
        void Reset();
    }
}