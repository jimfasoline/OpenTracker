﻿using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using OpenTracker.Models.NodeConnections;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenTracker.Models.RequirementNodes
{
    /// <summary>
    /// This class contains requirement node data.
    /// </summary>
    public class RequirementNode : IRequirementNode
    {
        private readonly IMode _mode;
        private readonly IRequirementNodeFactory _factory;

        private readonly RequirementNodeID _id;
        private readonly bool _start;

        private readonly List<INodeConnection> _connections =
            new List<INodeConnection>();

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? ChangePropagated;

        private bool _alwaysAccessible;
        public bool AlwaysAccessible
        {
            get => _alwaysAccessible;
            set
            {
                if (_alwaysAccessible != value)
                {
                    _alwaysAccessible = value;
                    UpdateAccessibility();
                }
            }
        }

        private int _exitsAccessible;
        public int ExitsAccessible
        {
            get => _exitsAccessible;
            set
            {
                if (_exitsAccessible != value)
                {
                    _exitsAccessible = value;
                    UpdateAccessibility();
                }
            }
        }

        private int _dungeonExitsAccessible;
        public int DungeonExitsAccessible
        {
            get => _dungeonExitsAccessible;
            set
            {
                if (_dungeonExitsAccessible != value)
                {
                    _dungeonExitsAccessible = value;
                    OnPropertyChanged(nameof(DungeonExitsAccessible));
                }
            }
        }

        private int _insanityExitsAccessible;
        public int InsanityExitsAccessible
        {
            get => _insanityExitsAccessible;
            set
            {
                if (_insanityExitsAccessible != value)
                {
                    _insanityExitsAccessible = value;
                    OnPropertyChanged(nameof(InsanityExitsAccessible));
                }
            }
        }

        private AccessibilityLevel _accessibility;
        public AccessibilityLevel Accessibility
        {
            get => _accessibility;
            private set
            {
                if (_accessibility != value)
                {
                    _accessibility = value;
                    OnPropertyChanged(nameof(Accessibility));
                }
            }
        }

        public delegate IRequirementNode Factory(RequirementNodeID id, bool start);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mode">
        /// The mode settings.
        /// </param>
        /// <param name="requirementNodes">
        /// The requirement node dictionary.
        /// </param>
        /// <param name="factory">
        /// The requirement node factory.
        /// </param>
        /// <param name="id">
        /// The requirement node identity.
        /// </param>
        /// <param name="start">
        /// A boolean representing whether the node is the start node.
        /// </param>
        public RequirementNode(
            IMode mode, IRequirementNodeDictionary requirementNodes,
            IRequirementNodeFactory factory, RequirementNodeID id, bool start)
        {
            _mode = mode;
            _factory = factory;
            _id = id;
            _start = start;
            AlwaysAccessible = _start;

            requirementNodes.ItemCreated += OnNodeCreated;
            _mode.PropertyChanged += OnModeChanged;
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

            if (propertyName == nameof(ExitsAccessible) ||
                propertyName == nameof(DungeonExitsAccessible) ||
                propertyName == nameof(AlwaysAccessible) ||
                propertyName == nameof(InsanityExitsAccessible))
            {
                UpdateAccessibility();
            }

            if (propertyName == nameof(Accessibility))
            {
                ChangePropagated?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Subscribes to the ItemCreated event on the IRequirementNodeDictionary interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the ItemCreated event.
        /// </param>
        private void OnNodeCreated(object? sender, KeyValuePair<RequirementNodeID, IRequirementNode> e)
        {
            if (e.Value == this)
            {
                var requirementNodes = ((IRequirementNodeDictionary)sender!);

                requirementNodes.ItemCreated -= OnNodeCreated;
                _factory.PopulateNodeConnections(e.Key, this, _connections);

                UpdateAccessibility();

                foreach (var connection in _connections)
                {
                    connection.PropertyChanged += OnConnectionChanged;
                }
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the IMode interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnModeChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IMode.EntranceShuffle))
            {
                UpdateAccessibility();
            }    
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the INodeConnection interface.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnConnectionChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(INodeConnection.Accessibility))
            {
                UpdateAccessibility();
            }
        }

        /// <summary>
        /// Updates the accessibility of the node.
        /// </summary>
        private void UpdateAccessibility()
        {
            Accessibility = GetNodeAccessibility(new List<IRequirementNode>());
        }

        /// <summary>
        /// Returns the node accessibility.
        /// </summary>
        /// <param name="excludedNodes">
        /// The list of node IDs from which to not check accessibility.
        /// </param>
        /// <returns>
        /// The accessibility of the node.
        /// </returns>
        public AccessibilityLevel GetNodeAccessibility(List<IRequirementNode> excludedNodes)
        {
            if (AlwaysAccessible ||
                (InsanityExitsAccessible > 0 &&
                _mode.EntranceShuffle >= EntranceShuffle.Insanity) ||
                (ExitsAccessible > 0 && _mode.EntranceShuffle >= EntranceShuffle.All) ||
                (DungeonExitsAccessible > 0 &&
                _mode.EntranceShuffle >= EntranceShuffle.Dungeon))
            {
                return AccessibilityLevel.Normal;
            }

            AccessibilityLevel finalAccessibility = AccessibilityLevel.None;

            foreach (var connection in _connections)
            {
                finalAccessibility = AccessibilityLevelMethods.Max(
                    finalAccessibility, connection.GetConnectionAccessibility(excludedNodes));

                if (finalAccessibility == AccessibilityLevel.Normal)
                {
                    break;
                }
            }

            return finalAccessibility;
        }

        /// <summary>
        /// Resets AlwaysAccessible property for testing purposes.
        /// </summary>
        public void Reset()
        {
            AlwaysAccessible = _start;
        }
    }
}
