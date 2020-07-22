﻿using OpenTracker.Interfaces;
using OpenTracker.Models.UndoRedo;
using OpenTracker.Models.Sections;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Text;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Items;

namespace OpenTracker.ViewModels.SmallItemControls
{
    /// <summary>
    /// This is the ViewModel of the small item control representing a dungeon prize.
    /// </summary>
    public class PrizeSmallItemControlVM : SmallItemControlVMBase, IClickHandler
    {
        private readonly IPrizeSection _section;

        public string ImageSource
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("avares://OpenTracker/Assets/Images/Items/");

                if (_section.PrizePlacement.Prize == null)
                {
                    sb.Append("unknown");
                }
                else
                {
                    sb.Append(_section.PrizePlacement.Prize.Type.ToString().ToLowerInvariant());
                }

                sb.Append(_section.IsAvailable() ? "0" : "1");
                sb.Append(".png");

                return sb.ToString();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="section">
        /// The prize section to be represented.
        /// </param>
        public PrizeSmallItemControlVM(IPrizeSection section)
        {
            _section = section ?? throw new ArgumentNullException(nameof(section));

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
        /// Change the prize of the dungeon.
        /// </summary>
        private void ChangePrize()
        {
            if (_section.PrizePlacement.Prize == null)
            {
                UndoRedoManager.Instance.Execute(new ChangePrize(_section.PrizePlacement,
                    ItemDictionary.Instance[ItemType.Crystal]));
            }
            else if (_section.PrizePlacement.Prize.Type == ItemType.GreenPendant)
            {
                UndoRedoManager.Instance.Execute(new ChangePrize(_section.PrizePlacement, null));
            }
            else
            {
                var newPrize = ItemDictionary.Instance[_section.PrizePlacement.Prize.Type + 1];
                UndoRedoManager.Instance.Execute(new ChangePrize(_section.PrizePlacement, newPrize));
            }
        }

        /// <summary>
        /// Handles left clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force = false)
        {
            UndoRedoManager.Instance.Execute(new TogglePrize(_section));
        }

        /// <summary>
        /// Handles right clicks.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force = false)
        {
            ChangePrize();
        }
    }
}
