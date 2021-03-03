﻿using Avalonia.Input;
using OpenTracker.Models.BossPlacements;
using OpenTracker.Models.Modes;
using OpenTracker.Utils;
using OpenTracker.ViewModels.BossSelect;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;

namespace OpenTracker.ViewModels.PinnedLocations.Sections
{
    /// <summary>
    /// This class contains boss section icon control ViewModel data.
    /// </summary>
    public class BossSectionIconVM : ViewModelBase, ISectionIconVMBase
    {
        private readonly IMode _mode;
        private readonly IBossPlacement _bossPlacement;

        public bool Visible =>
            _mode.BossShuffle;
        public string ImageSource =>
            _bossPlacement.Boss.HasValue ?
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossPlacement.Boss.ToString()!.ToLowerInvariant()}1.png" :
            "avares://OpenTracker/Assets/Images/Bosses/" +
            $"{_bossPlacement.DefaultBoss.ToString().ToLowerInvariant()}0.png";

        public IBossSelectPopupVM BossSelect { get; }
        
        public ReactiveCommand<PointerReleasedEventArgs, Unit> HandleClickCommand { get; }

        public delegate BossSectionIconVM Factory(IBossPlacement bossPlacement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings data.
        /// </param>
        /// <param name="bossSelectFactory">
        /// An Autofac factory for creating boss select controls.
        /// </param>
        /// <param name="bossPlacement">
        /// The boss section to be represented.
        /// </param>
        public BossSectionIconVM(
            IMode mode, IBossSelectPopupVM.Factory bossSelectFactory, IBossPlacement bossPlacement)
        {
            _mode = mode;
            _bossPlacement = bossPlacement;
            BossSelect = bossSelectFactory(bossPlacement);
            
            HandleClickCommand = ReactiveCommand.Create<PointerReleasedEventArgs>(HandleClick);

            _mode.PropertyChanged += OnModeChanged;
            _bossPlacement.PropertyChanged += OnBossChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the Mode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Mode.BossShuffle))
            {
                this.RaisePropertyChanged(nameof(Visible));
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IBossSection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnBossChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IBossPlacement.Boss))
            {
                this.RaisePropertyChanged(nameof(ImageSource));
            }
        }

        /// <summary>
        /// Opens the boss select popup.
        /// </summary>
        private void OpenBossSelect()
        {
            BossSelect.PopupOpen = true;
        }

        /// <summary>
        /// Handles clicking the control.
        /// </summary>
        /// <param name="e">
        /// The PointerReleased event args.
        /// </param>
        private void HandleClick(PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                OpenBossSelect();
            }
        }
    }
}
