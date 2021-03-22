﻿using OpenTracker.Models.Locations;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.UndoRedo;

namespace OpenTracker.Models.Connections
{
    /// <summary>
    /// This interface contains the map location connection data.
    /// </summary>
    public interface IConnection
    {
        IMapLocation Location1 { get; }
        IMapLocation Location2 { get; }

        delegate IConnection Factory(IMapLocation location1, IMapLocation location2);

        bool Equals(object obj);
        int GetHashCode();
        ConnectionSaveData Save();

        /// <summary>
        /// Creates an undoable action to remove the connection and sends it to the undo/redo manager.
        /// </summary>
        IUndoable CreateRemoveConnectionAction();
    }
}