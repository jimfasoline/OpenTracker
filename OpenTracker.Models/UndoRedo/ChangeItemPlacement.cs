﻿using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo
{
    /// <summary>
    /// This class contains undoable action data to change the item placement setting.
    /// </summary>
    public class ChangeItemPlacement : IUndoable
    {
        private readonly IMode _mode;
        private readonly ItemPlacement _itemPlacement;
        private ItemPlacement _previousItemPlacement;

        public delegate ChangeItemPlacement Factory(ItemPlacement itemPlacement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="itemPlacement">
        /// The new item placement setting.
        /// </param>
        public ChangeItemPlacement(IMode mode, ItemPlacement itemPlacement)
        {
            _mode = mode;
            _itemPlacement = itemPlacement;
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
            _previousItemPlacement = _mode.ItemPlacement;
            _mode.ItemPlacement = _itemPlacement;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        public void ExecuteUndo()
        {
            _mode.ItemPlacement = _previousItemPlacement;
        }
    }
}
