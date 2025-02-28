﻿using OpenTracker.Models.Dungeons;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.KeyDoors
{
    /// <summary>
    /// This interface contains the dictionary container for key door .
    /// </summary>
    public interface IKeyDoorDictionary : IDictionary<KeyDoorID, IKeyDoor>,
        ICollection<KeyValuePair<KeyDoorID, IKeyDoor>>
    {
        event EventHandler<KeyValuePair<KeyDoorID, IKeyDoor>> ItemCreated;

        delegate IKeyDoorDictionary Factory(IMutableDungeon dungeonData);
    }
}