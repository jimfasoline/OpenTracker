﻿using OpenTracker.Models.SaveLoad;
using System.ComponentModel;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This interface contains item data.
    /// </summary>
    public interface IItem : INotifyPropertyChanged, ISaveable<ItemSaveData>
    {
        int Current { get; set; }

        void Add();
        bool CanAdd();
        bool CanRemove();
        void Remove();
        void Reset();
    }
}