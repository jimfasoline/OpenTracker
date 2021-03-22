﻿using System;
using System.Collections.Generic;
using System.Linq;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;

namespace OpenTracker.Models.Items
{
    /// <summary>
    /// This class contains the dictionary container for item data.
    /// </summary>
    public class ItemDictionary : LazyDictionary<ItemType, IItem>, IItemDictionary
    {
        private readonly Lazy<IItemFactory> _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">
        /// Factory for creating items.
        /// </param>
        public ItemDictionary(IItemFactory.Factory factory) : base(new Dictionary<ItemType, IItem>())
        {
            _factory = new Lazy<IItemFactory>(() => factory());
        }

        protected override IItem Create(ItemType key)
        {
            return _factory.Value.GetItem(key);
        }

        /// <summary>
        /// Resets all contained items to their starting values.
        /// </summary>
        public void Reset()
        {
            foreach (var item in Values)
            {
                item.Reset();
            }
        }

        /// <summary>
        /// Returns a dictionary of item save data.
        /// </summary>
        /// <returns>
        /// A dictionary of item save data.
        /// </returns>
        public Dictionary<ItemType, ItemSaveData> Save()
        {
            return Keys.ToDictionary(type => type, type => this[type].Save());
        }

        /// <summary>
        /// Loads a dictionary of item save data.
        /// </summary>
        public void Load(Dictionary<ItemType, ItemSaveData>? saveData)
        {
            if (saveData == null)
            {
                return;
            }

            foreach (var item in saveData.Keys)
            {
                this[item].Load(saveData[item]);
            }
        }
    }
}
