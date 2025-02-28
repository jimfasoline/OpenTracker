﻿using OpenTracker.Models.Locations;

namespace OpenTracker.Models.SaveLoad
{
    /// <summary>
    /// This class contains connection save data.
    /// </summary>
    public class ConnectionSaveData
    {
        public LocationID Location1 { get; set; }
        public LocationID Location2 { get; set; }
        public int Index1 { get; set; }
        public int Index2 { get; set; }
    }
}
