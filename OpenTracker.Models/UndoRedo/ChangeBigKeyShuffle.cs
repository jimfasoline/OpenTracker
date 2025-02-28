﻿using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the big key shuffle setting.
    /// </summary>
    public class ChangeBigKeyShuffle : IUndoable
    {
        private readonly IMode _mode;

        private readonly bool _bigKeyShuffle;

        private bool _previousBigKeyShuffle;

        public delegate ChangeBigKeyShuffle Factory(bool bigKeyShuffle);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="bigKeyShuffle">
        /// The new big key shuffle setting.
        /// </param>
        public ChangeBigKeyShuffle(IMode mode, bool bigKeyShuffle)
        {
            _mode = mode;
            _bigKeyShuffle = bigKeyShuffle;
        }

        /// <summary>
        /// Returns whether the action can be executed.
        /// </summary>
        /// <returns>
        /// A boolean representing whether the action can be executed.
        /// </returns>
        public bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        public void ExecuteDo()
        {
            _previousBigKeyShuffle = _mode.BigKeyShuffle;
            _mode.BigKeyShuffle = _bigKeyShuffle;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.BigKeyShuffle = _previousBigKeyShuffle;
        }
    }
}
