﻿using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action to change the guaranteed boss items setting (Ambrosia).
    /// </summary>
    public class ChangeGuaranteedBossItems : IUndoable
    {
        private readonly IMode _mode;
        private readonly bool _guaranteedBossItems;

        private bool _previousGuaranteedBossItems;

        public delegate ChangeGuaranteedBossItems Factory(bool guaranteedBossItems);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="guaranteedBossItems">
        /// The new guaranteed boss items setting.
        /// </param>
        public ChangeGuaranteedBossItems(IMode mode, bool guaranteedBossItems)
        {
            _mode = mode;
            _guaranteedBossItems = guaranteedBossItems;
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
            _previousGuaranteedBossItems = _mode.GuaranteedBossItems;
            _mode.GuaranteedBossItems = _guaranteedBossItems;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.GuaranteedBossItems = _previousGuaranteedBossItems;
        }
    }
}
