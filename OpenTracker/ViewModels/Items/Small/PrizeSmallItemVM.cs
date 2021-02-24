﻿using OpenTracker.Interfaces;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.Sections;
using ReactiveUI;
using System.ComponentModel;
using System.Text;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Items;
using System.Linq;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This is the ViewModel of the small item control representing a dungeon prize.
    /// </summary>
    public class PrizeSmallItemVM : ViewModelBase, ISmallItemVMBase, IClickHandler
    {
        private readonly IPrizeDictionary _prizes;
        private readonly IUndoRedoManager _undoRedoManager;
        private readonly IUndoableFactory _undoableFactory;

        private readonly IPrizeSection _section;

        public string ImageSource
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("avares://OpenTracker/Assets/Images/Prizes/");

                if (_section.PrizePlacement.Prize == null)
                {
                    sb.Append("unknown");
                }
                else
                {
                    sb.Append(
                        _prizes.FirstOrDefault(
                            x => x.Value == _section.PrizePlacement.Prize).Key.ToString()
                                .ToLowerInvariant());
                }

                sb.Append(_section.IsAvailable() ? "0" : "1");
                sb.Append(".png");

                return sb.ToString();
            }
        }

        public delegate PrizeSmallItemVM Factory(IPrizeSection section);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The prize section to be represented.
        /// </param>
        public PrizeSmallItemVM(
            IPrizeDictionary prizes, IUndoRedoManager undoRedoManager,
            IUndoableFactory undoableFactory, IPrizeSection section)
        {
            _prizes = prizes;
            _undoRedoManager = undoRedoManager;
            _undoableFactory = undoableFactory;

            _section = section;

            _section.PropertyChanged += OnSectionChanged;
            _section.PrizePlacement.PropertyChanged += OnPrizeChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IPrizePlacement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnPrizeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IPrizePlacement.Prize))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the ISection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnSectionChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ISection.Available))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
        }

        /// <summary>
        /// Handles left clicks and toggles the prize section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            _undoRedoManager.Execute(_undoableFactory.GetTogglePrize(_section, true));
        }

        /// <summary>
        /// Handles right clicks and changes the prize.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            _undoRedoManager.Execute(_undoableFactory.GetChangePrize(_section.PrizePlacement));
        }
    }
}
