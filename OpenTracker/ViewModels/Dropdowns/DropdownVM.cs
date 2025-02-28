﻿using Avalonia.Input;
using OpenTracker.Models.Dropdowns;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;
using Avalonia.Threading;

namespace OpenTracker.ViewModels.Dropdowns
{
    /// <summary>
    /// This class contains the dropdown icon control ViewModel data.
    /// </summary>
    public class DropdownVM : ViewModelBase, IDropdownVM
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IDropdown _dropdown;
        private readonly string _imageSourceBase;

        public bool Visible => _dropdown.RequirementMet;
        public string ImageSource => _imageSourceBase + (_dropdown.Checked ? "1" : "0") + ".png";
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="undoRedoManager">
        /// The undo/redo manager.
        /// </param>
        /// <param name="undoableFactory">
        /// The factory for creating undoable actions.
        /// </param>
        /// <param name="imageSourceBase">
        /// A string representing the base image source.
        /// </param>
        /// <param name="dropdown">
        /// An item that is to be represented by this control.
        /// </param>
        public DropdownVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory, IDropdown dropdown,
            string imageSourceBase)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _dropdown = dropdown;
            _imageSourceBase = imageSourceBase;

            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _dropdown.PropertyChanged += OnDropdownChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IDropdown interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnDropdownChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IDropdown.RequirementMet):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
                    break;
                case nameof(IDropdown.Checked):
                    await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
                    break;
            }
        }

        /// <summary>
        /// Creates a new undoable action to check the dropdown to the undo/redo manager.
        /// </summary>
        private void CheckDropdown()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetCheckDropdown(_dropdown));
        }

        /// <summary>
        /// Creates a new undoable action to uncheck the dropdown to the undo/redo manager.
        /// </summary>
        private void UncheckDropdown()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetUncheckDropdown(_dropdown));
        }

        /// <summary>
        /// Handles the dropdown being clicked.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
        /// </param>
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    CheckDropdown();
                    break;
                case MouseButton.Right:
                    UncheckDropdown();
                    break;
            }
        }
    }
}
