﻿using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.NodeConnections
{
    /// <summary>
    /// This class contains dungeon entry node connection data.
    /// </summary>
    public class EntryNodeConnection : INodeConnection
    {
        private readonly IRequirementNode _fromNode;

        public IRequirement Requirement { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public AccessibilityLevel Accessibility =>
            _fromNode.Accessibility;

        public delegate EntryNodeConnection Factory(IRequirementNode fromNode);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirements">
        /// The requirement dictionary.
        /// </param>
        /// <param name="fromNode">
        /// The node from which the connection originates.
        /// </param>
        public EntryNodeConnection(
            IRequirementDictionary requirements, IRequirementNode fromNode)
        {
            _fromNode = fromNode;

            Requirement = requirements[RequirementType.NoRequirement];

            _fromNode.PropertyChanged += OnNodeChanged;
        }

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">
        /// The string of the property name of the changed property.
        /// </param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IRequirementNode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
            {
                OnPropertyChanged(nameof(Accessibility));
            }
        }

        /// <summary>
        /// Returns the availability of the connection, excluding loops from the specified nodes.
        /// </summary>
        /// <param name="excludedNodes">
        /// A list of nodes to exclude to prevent loops.
        /// </param>
        /// <returns>
        /// The availability of the connection.
        /// </returns>
        public AccessibilityLevel GetConnectionAccessibility(List<IRequirementNode> excludedNodes)
        {
            if (excludedNodes == null)
            {
                throw new ArgumentNullException(nameof(excludedNodes));
            }

            return Accessibility;
        }
    }
}
