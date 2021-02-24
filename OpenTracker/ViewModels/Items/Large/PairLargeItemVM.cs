﻿using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Globalization;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This is the ViewModel for the large Items panel control representing a pair of items.
    /// </summary>
    public class PairLargeItemVM : ViewModelBase, ILargeItemVMBase, IClickHandler
    {
        private readonly IUndoRedoManager _undoRedoManager;

        private readonly IItem[] _items;
        private readonly string _imageSourceBase;

        public string ImageSource =>
            _imageSourceBase + _items[0].Current.ToString(CultureInfo.InvariantCulture) +
            $"{_items[1].Current.ToString(CultureInfo.InvariantCulture)}.png";

        public delegate PairLargeItemVM Factory(IItem[] items, string imageSourceBase);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imageSourceBase">
        /// A string representing the image source base.
        /// </param>
        /// <param name="items">
        /// An array of items that are to be represented by this control.
        /// </param>
        public PairLargeItemVM(
            IUndoRedoManager undoRedoManager, IItem[] items, string imageSourceBase)
        {
            _undoRedoManager = undoRedoManager;

            _items = items;
            _imageSourceBase = imageSourceBase;

            if (_items.Length != 2)
            {
                throw new ArgumentOutOfRangeException(nameof(items));
            }

            foreach (var item in _items)
            {
                item.PropertyChanged += OnItemChanged;
            }
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
        private void OnItemChanged(object sender, PropertyChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Handles left clicks and cycles the first item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            _undoRedoManager.Execute(new CycleItem(_items[0]));
        }

        /// <summary>
        /// Handles right clicks and cycles the second item.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            _undoRedoManager.Execute(new CycleItem(_items[1]));
        }
    }
}
