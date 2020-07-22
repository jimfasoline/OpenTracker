﻿using OpenTracker.Interfaces;
using OpenTracker.Models.Items;
using OpenTracker.Models.PrizePlacements;
using OpenTracker.Models.Sections;
using OpenTracker.Models.UndoRedo;
using ReactiveUI;
using System;
using System.ComponentModel;
using System.Text;

namespace OpenTracker.ViewModels.SectionControls
{
    /// <summary>
    /// This is the ViewModel of the section icon control representing a prize section.
    /// </summary>
    public class PrizeSectionIconControlVM : SectionIconControlVMBase, IClickHandler
    {
        private readonly IPrizeSection _section;

        public string ImageSource
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("avares://OpenTracker/Assets/Images/Items/");

                if (_section.PrizePlacement.Prize == null)
                {
                    sb.Append("unknown");
                }
                else if (_section.PrizePlacement.Prize.Type == ItemType.Aga2)
                {
                    sb.Append("aga");
                }
                else
                {
                    sb.Append(_section.PrizePlacement.Prize.Type.ToString().ToLowerInvariant());
                }

                sb.Append(_section.IsAvailable() ? "0.png" : "1.png");

                return sb.ToString();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prizeSection">
        /// The prize section to be presented.
        /// </param>
        public PrizeSectionIconControlVM(IPrizeSection prizeSection)
        {
            _section = prizeSection ?? throw new ArgumentNullException(nameof(prizeSection));

            _section.PropertyChanged += OnSectionChanged;
            _section.PrizePlacement.PropertyChanged += OnPrizeChanged; 
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
                UpdateImage();
            }
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
                UpdateImage();
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event for the ImageSource property.
        /// </summary>
        private void UpdateImage()
        {
            this.RaisePropertyChanged(nameof(ImageSource));
        }

        /// <summary>
        /// Handles left clicks and collects the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnLeftClick(bool force)
        {
            UndoRedoManager.Instance.Execute(new CollectSection(_section, force));
        }

        /// <summary>
        /// Handles right clicks and uncollects the section.
        /// </summary>
        /// <param name="force">
        /// A boolean representing whether the logic should be ignored.
        /// </param>
        public void OnRightClick(bool force)
        {
            UndoRedoManager.Instance.Execute(new UncollectSection(_section));
        }
    }
}
