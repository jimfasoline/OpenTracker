﻿using Avalonia.Threading;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using ReactiveUI;
using System.ComponentModel;

namespace OpenTracker.ViewModels.Items.Small
{
    /// <summary>
    /// This class contains the reserved space small items panel control ViewModel data.
    /// </summary>
    public class SpacerSmallItemVM : ViewModelBase, ISmallItemVMBase
    {
        private readonly IRequirement _requirement;

        public bool Visible => _requirement.Met;

        public delegate SpacerSmallItemVM Factory(IRequirement requirement);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirement">
        /// The requirement to be checked for whether the space is reserved.
        /// </param>
        public SpacerSmallItemVM(IRequirement requirement)
        {
            _requirement = requirement;

            _requirement.PropertyChanged += OnRequirementChanged;
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirement interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private async void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Visible)));
        }
    }
}
