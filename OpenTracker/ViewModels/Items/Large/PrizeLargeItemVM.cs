﻿using Avalonia.Input;
using Avalonia.Threading;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.Items.Large
{
    /// <summary>
    /// This class contains the dungeon prize large items panel control ViewModel data.
    /// </summary>
    public class PrizeLargeItemVM : ViewModelBase, ILargeItemVMBase
    {
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IPrizeSection _section;
        private readonly string _imageSourceBase;

        public string ImageSource => _imageSourceBase + (_section.IsAvailable() ? "0.png" : "1.png");
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClick { get; }

        public delegate PrizeLargeItemVM Factory(IPrizeSection section, string imageSourceBase);

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
        /// <param name="section">
        /// An item that is to be represented by this control.
        /// </param>
        public PrizeLargeItemVM(
            IUndoRedoManager undoRedoManager, IUndoableFactory undoableFactory, IPrizeSection section,
            string imageSourceBase)
        {
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _section = section;
            _imageSourceBase = imageSourceBase;
            
            HandleClick = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClickImpl);

            _section.PropertyChanged += OnSectionChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IPrizeSection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Available))
            {
                await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(ImageSource)));
            }
        }

        /// <summary>
        /// Creates an undoable action to collect the prize section, ignoring logic, and sends it to the undo/redo
        /// manager.
        /// </summary>
        private void CollectSection()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetCollectSection(_section, true));
        }

        /// <summary>
        /// Creates an undoable action to un-collect the prize section and sends it to the undo/redo manager.
        /// </summary>
        private void UncollectSection()
        {
            _undoRedoManager.NewAction(_undoableFactory.GetUncollectSection(_section));
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The pointer released event args.
        /// </param>
        private void HandleClickImpl(PointerReleasedEventArgs e)
        {
            switch (e.InitialPressMouseButton)
            {
                case MouseButton.Left:
                    CollectSection();
                    break;
                case MouseButton.Right:
                    UncollectSection();
                    break;
            }
        }
    }
}
