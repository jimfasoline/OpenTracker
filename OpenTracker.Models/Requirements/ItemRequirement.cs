﻿using OpenTracker.Models.Items;
using System.ComponentModel;

namespace OpenTracker.Models.Requirements
{
    /// <summary>
    /// This class contains item requirement data.
    /// </summary>
    public class ItemRequirement : BooleanRequirement
    {
        private readonly IItem _item;
        private readonly int _count;

        public delegate ItemRequirement Factory(IItem item, int count = 1);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">
        /// The item of the requirement.
        /// </param>
        /// <param name="count">
        /// A 32-bit integer representing the number of the item required.
        /// </param>
        public ItemRequirement(IItem item, int count = 1)
        {
            _item = item;
            _count = count;

            _item.PropertyChanged += OnItemChanged;

            UpdateValue();
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IItem interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnItemChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateValue();
            }
        }

        protected override bool ConditionMet()
        {
            return _item.Current >= _count;
        }
    }
}
