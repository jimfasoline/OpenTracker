﻿using OpenTracker.Models.DungeonItems;
using OpenTracker.Models.DungeonNodes;
using OpenTracker.Models.Items;
using OpenTracker.Models.KeyDoors;
using OpenTracker.Models.KeyLayouts;
using OpenTracker.Models.Locations;
using OpenTracker.Models.Modes;
using OpenTracker.Models.RequirementNodes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.Sections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenTracker.Models.Dungeons
{
    /// <summary>
    /// This is the class containing dungeon data.
    /// </summary>
    public class Dungeon : Location, IDungeon
    {
        private readonly List<IRequirement> _subscribedRequirements =
            new List<IRequirement>();
        private readonly List<DungeonNodeID> _nodes;

        public event EventHandler<IMutableDungeon> DungeonDataCreated;

        public int Map { get; }
        public int Compass { get; }
        public int SmallKeys { get; }
        public int BigKey { get; }

        public IItem SmallKeyItem { get; }
        public IItem BigKeyItem { get; }

        public List<DungeonItemID> Items { get; }
        public List<DungeonItemID> Bosses { get; }
        public List<KeyDoorID> SmallKeyDoors { get; }
        public List<KeyDoorID> BigKeyDoors { get; }
        public List<IKeyLayout> KeyLayouts { get; }
        public ConcurrentQueue<IMutableDungeon> DungeonDataQueue { get; } =
            new ConcurrentQueue<IMutableDungeon>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">
        /// The ID of the location.
        /// </param>
        /// <param name="name">
        /// A string representing the name of the dungeon.
        /// </param>
        /// <param name="mapLocations">
        /// A list of map locations.
        /// </param>
        /// <param name="map">
        /// A 32-bit signed integer representing the number of maps in the dungeon.
        /// </param>
        /// <param name="compass">
        /// A 32-bit signed integer representing the number of compasses in the dungeon.
        /// </param>
        /// <param name="smallKeys">
        /// A 32-bit signed integer representing the number of small keys in the dungeon.
        /// </param>
        /// <param name="bigKey">
        /// A 32-bit signed integer representing the number of big keys in the dungeon.
        /// </param>
        /// <param name="smallKeyItem">
        /// The small key item.
        /// </param>
        /// <param name="bigKeyItem">
        /// The big key item.
        /// </param>
        /// <param name="nodes">
        /// A list of dungeon node IDs within the dungeon.
        /// </param>
        /// <param name="items">
        /// A list of dungeon item IDs within the dungeon.
        /// </param>
        /// <param name="bosses">
        /// A list of dungeon item IDs for bosses within the dungeon.
        /// </param>
        /// <param name="smallKeyDoors">
        /// A list of small key door IDs within the dungeon.
        /// </param>
        /// <param name="bigKeyDoors">
        /// A list of big key door IDs within the dungeon.
        /// </param>
        public Dungeon(
            LocationID id, string name, List<MapLocation> mapLocations, int map, int compass,
            int smallKeys, int bigKey, IItem smallKeyItem, IItem bigKeyItem,
            List<DungeonNodeID> nodes, List<DungeonItemID> items, List<DungeonItemID> bosses,
            List<KeyDoorID> smallKeyDoors, List<KeyDoorID> bigKeyDoors)
            : base(id, name, mapLocations)
        {
            Map = map;
            Compass = compass;
            SmallKeys = smallKeys;
            BigKey = bigKey;
            SmallKeyItem = smallKeyItem;
            BigKeyItem = bigKeyItem;
            _nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
            Items = items ?? throw new ArgumentNullException(nameof(items));
            Bosses = bosses ?? throw new ArgumentNullException(nameof(bosses));
            SmallKeyDoors = smallKeyDoors ?? throw new ArgumentNullException(nameof(smallKeyDoors));
            BigKeyDoors = bigKeyDoors ?? throw new ArgumentNullException(nameof(bigKeyDoors));
            KeyLayouts = KeyLayoutFactory.GetDungeonKeyLayouts(this);

            for (int i = 0; i < Environment.ProcessorCount - 1; i++)
            {
                CreateDungeonData();
            }

            foreach (var section in Sections)
            {
                section.PropertyChanged += OnSectionChanged;
            }

            Mode.Instance.PropertyChanged += OnModeChanged;
            SubscribeToKeyItems();
            SubscribeToEntryConnections();
            SubscribeToConnectionRequirements();
            UpdateSectionAccessibility();
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
            if (e.PropertyName == nameof(IItem.Current))
            {
                UpdateSectionAccessibility();
            }
        }

        /// <summary>
        /// Subscribes to the PropertyChanged event on the RequirementNode class.
        /// </summary>
        /// <param name="sender">
        /// The sending object of the event.
        /// </param>
        /// <param name="e">
        /// The arguments of the PropertyChanged event.
        /// </param>
        private void OnNodeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirementNode.Accessibility))
            {
                UpdateSectionAccessibility();
            }
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
        private void OnRequirementChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IRequirement.Accessibility))
            {
                UpdateSectionAccessibility();
            }
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
            if (e.PropertyName == nameof(Mode.WorldState) ||
                e.PropertyName == nameof(Mode.DungeonItemShuffle))
            {
                UpdateSectionAccessibility();
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
                UpdateSectionAccessibility();
            }
        }

        /// <summary>
        /// Creates an instance of the mutable dungeon data.
        /// </summary>
        private void CreateDungeonData()
        {
            IMutableDungeon dungeonData = MutableDungeonFactory.GetMutableDungeon(this);
            DungeonDataQueue.Enqueue(dungeonData);
            DungeonDataCreated?.Invoke(this, dungeonData);
        }

        /// <summary>
        /// Subscribes to PropertyChanged event on each key item.
        /// </summary>
        private void SubscribeToKeyItems()
        {
            if (SmallKeyItem != null)
            {
                SmallKeyItem.PropertyChanged += OnItemChanged;
                ItemDictionary.Instance[ItemType.SmallKey].PropertyChanged += OnItemChanged;
            }

            if (BigKeyItem != null)
            {
                BigKeyItem.PropertyChanged += OnItemChanged;
            }
        }

        /// <summary>
        /// Subscribes to PropertyChanged event on each entry connection node.
        /// </summary>
        private void SubscribeToEntryConnections()
        {
            var subscribedNodes = new List<IRequirementNode>();

            foreach (var node in _nodes)
            {
                foreach (var connection in DungeonNodeFactory.GetDungeonEntryConnections(node))
                {
                    var fromNode = RequirementNodeDictionary.Instance[connection.FromNode];
                    var requirement = connection.Requirement;

                    if (!subscribedNodes.Contains(fromNode))
                    {
                        fromNode.PropertyChanged += OnNodeChanged;
                        subscribedNodes.Add(fromNode);
                    }

                    if (!_subscribedRequirements.Contains(requirement))
                    {
                        requirement.PropertyChanged += OnRequirementChanged;
                        _subscribedRequirements.Add(requirement);
                    }
                }    
            }
        }

        /// <summary>
        /// Subscribes to PropertyChanged event on each requirement.
        /// </summary>
        private void SubscribeToConnectionRequirements()
        {
            foreach (var node in _nodes)
            {
                foreach (var connection in DungeonNodeFactory.GetDungeonConnections(node))
                {
                    var requirement = connection.Requirement;

                    if (!_subscribedRequirements.Contains(requirement))
                    {
                        requirement.PropertyChanged += OnRequirementChanged;
                        _subscribedRequirements.Add(requirement);
                    }
                }
            }
        }

        /// <summary>
        /// Returns the next available DungeonData class in the queue.
        /// </summary>
        /// <returns>
        /// The next available DungeonData class.
        /// </returns>
        private IMutableDungeon GetDungeonData()
        {
            while (true)
            {
                if (DungeonDataQueue.TryDequeue(out IMutableDungeon result))
                {
                    return result;
                }
            }
        }

        /// <summary>
        /// Returns a list of numbers of small keys that could be collected.
        /// </summary>
        /// <returns>
        /// A list of 32-bit signed integers.
        /// </returns>
        private List<int> GetSmallKeyValues()
        {
            List<int> smallKeyValues = new List<int>();

            if (Mode.Instance.SmallKeyShuffle)
            {
                if (SmallKeyItem != null)
                {
                    smallKeyValues.Add(Math.Min(SmallKeys, SmallKeyItem.Current +
                        (Mode.Instance.WorldState == WorldState.Retro ?
                        ItemDictionary.Instance[ItemType.SmallKey].Current : 0)));
                }
                else
                {
                    smallKeyValues.Add(0);
                }
            }
            else
            {
                for (int i = 0; i <= SmallKeys; i++)
                {
                    smallKeyValues.Add(i);
                }
            }

            return smallKeyValues;
        }

        /// <summary>
        /// Returns a list of possible states for big key collection.
        /// </summary>
        /// <returns>
        /// A list of boolean values.
        /// </returns>
        private List<bool> GetBigKeyValues()
        {
            List<bool> bigKeyValues = new List<bool>();

            if (Mode.Instance.BigKeyShuffle)
            {
                if (BigKeyItem != null)
                {
                    bigKeyValues.Add(BigKeyItem.Current > 0);
                }
                else
                {
                    bigKeyValues.Add(false);
                }
            }
            else
            {
                bigKeyValues.Add(false);
                
                if (BigKey > 0)
                {
                    bigKeyValues.Add(true);
                }
            }

            return bigKeyValues;
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items for the contained sections.
        /// </summary>
        private void UpdateSectionAccessibility()
        {
            if (ID == LocationID.GanonsTower)
            {
                UpdateSectionAccessibilityParallel();
            }
            else
            {
                UpdateSectionAccessibilitySerial();
            }
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items for the contained sections.
        /// </summary>
        private void UpdateSectionAccessibilitySerial()
        {
            List<BlockingCollection<(List<KeyDoorID>, int, bool)>> keyDoorPermutationQueue =
                new List<BlockingCollection<(List<KeyDoorID>, int, bool)>>();
            BlockingCollection<(List<KeyDoorID>, int, bool)> finalKeyDoorPermutationQueue =
                new BlockingCollection<(List<KeyDoorID>, int, bool)>();
            BlockingCollection<(List<AccessibilityLevel>, AccessibilityLevel, int)> resultQueue =
                new BlockingCollection<(List<AccessibilityLevel>, AccessibilityLevel, int)>();

            for (int i = 0; i <= SmallKeyDoors.Count; i++)
            {
                keyDoorPermutationQueue.Add(new BlockingCollection<(List<KeyDoorID>, int, bool)>());
            }

            List<int> smallKeyValues = GetSmallKeyValues();
            List<bool> bigKeyValues = GetBigKeyValues();

            foreach (int smallKeyValue in smallKeyValues)
            {
                foreach (bool bigKeyValue in bigKeyValues)
                {
                    keyDoorPermutationQueue[0].Add((new List<KeyDoorID>(), smallKeyValue, bigKeyValue));
                }
            }

            keyDoorPermutationQueue[0].CompleteAdding();

            for (int i = 0; i < keyDoorPermutationQueue.Count; i++)
            {
                foreach (var item in keyDoorPermutationQueue[i].GetConsumingEnumerable())
                {
                    IMutableDungeon dungeonData = GetDungeonData();
                    
                    dungeonData.SetSmallKeyDoorState(item.Item1);
                    dungeonData.SetBigKeyDoorState(item.Item3);
                    
                    int availableKeys = dungeonData.GetFreeKeys() + item.Item2 - item.Item1.Count;

                    if (availableKeys == 0)
                    {
                        finalKeyDoorPermutationQueue.Add(item);
                        DungeonDataQueue.Enqueue(dungeonData);
                        continue;
                    }
                    
                    List<KeyDoorID> accessibleKeyDoors = dungeonData.GetAccessibleKeyDoors();
                    
                    if (accessibleKeyDoors.Count == 0)
                    {
                        finalKeyDoorPermutationQueue.Add(item);
                        DungeonDataQueue.Enqueue(dungeonData);
                        continue;
                    }
                    
                    foreach (KeyDoorID keyDoor in accessibleKeyDoors)
                    {
                        List<KeyDoorID> newPermutation = item.Item1.GetRange(0, item.Item1.Count);
                        newPermutation.Add(keyDoor);
                        keyDoorPermutationQueue[i + 1].Add((newPermutation, item.Item2,
                            item.Item3));
                    }
                    
                    DungeonDataQueue.Enqueue(dungeonData);
                }

                keyDoorPermutationQueue[i].Dispose();

                if (i + 1 < keyDoorPermutationQueue.Count)
                {
                    keyDoorPermutationQueue[i + 1].CompleteAdding();
                }
                else
                {
                    finalKeyDoorPermutationQueue.CompleteAdding();
                }
            }

            foreach (var item in finalKeyDoorPermutationQueue.GetConsumingEnumerable())
            {
                IMutableDungeon dungeonData = GetDungeonData();

                dungeonData.SetSmallKeyDoorState(item.Item1);
                dungeonData.SetBigKeyDoorState(item.Item3);

                if (!dungeonData.ValidateKeyLayout(item.Item2, item.Item3))
                {
                    DungeonDataQueue.Enqueue(dungeonData);
                    continue;
                }

                List<AccessibilityLevel> bossAccessibility = dungeonData.GetBossAccessibility();

                (AccessibilityLevel, int) accessibility =
                    dungeonData.GetItemAccessibility(item.Item2, item.Item3);

                resultQueue.Add((bossAccessibility, accessibility.Item1, accessibility.Item2));

                DungeonDataQueue.Enqueue(dungeonData);
            }

            finalKeyDoorPermutationQueue.Dispose();
            resultQueue.CompleteAdding();

            List<AccessibilityLevel> lowestBossAccessibilities = new List<AccessibilityLevel>();
            List<AccessibilityLevel> highestBossAccessibilities = new List<AccessibilityLevel>();

            for (int i = 0; i < Bosses.Count; i++)
            {
                lowestBossAccessibilities.Add(AccessibilityLevel.Normal);
                highestBossAccessibilities.Add(AccessibilityLevel.None);
            }

            AccessibilityLevel lowestAccessibility = AccessibilityLevel.Normal;
            AccessibilityLevel highestAccessibility = AccessibilityLevel.None;
            int highestAccessible = 0;

            foreach (var item in resultQueue.GetConsumingEnumerable())
            {
                for (int i = 0; i < item.Item1.Count; i++)
                {
                    if (item.Item1[i] < lowestBossAccessibilities[i])
                    {
                        lowestBossAccessibilities[i] = item.Item1[i];
                    }

                    if (item.Item1[i] > highestBossAccessibilities[i])
                    {
                        highestBossAccessibilities[i] = item.Item1[i];
                    }
                }

                if (item.Item2 < lowestAccessibility)
                {
                    lowestAccessibility = item.Item2;
                }

                if (item.Item2 > highestAccessibility)
                {
                    highestAccessibility = item.Item2;
                }

                if (item.Item3 > highestAccessible)
                {
                    highestAccessible = item.Item3;
                }
            }

            resultQueue.Dispose();

            AccessibilityLevel finalAccessibility = highestAccessibility;

            if (finalAccessibility == AccessibilityLevel.Normal &&
                lowestAccessibility < AccessibilityLevel.Normal)
            {
                finalAccessibility = AccessibilityLevel.SequenceBreak;
            }

            switch (finalAccessibility)
            {
                case AccessibilityLevel.None:
                    {
                        (Sections[0] as IDungeonItemSection).Accessibility = finalAccessibility;
                        (Sections[0] as IDungeonItemSection).Accessible = 0;
                    }
                    break;
                case AccessibilityLevel.Inspect:
                case AccessibilityLevel.Partial:
                    {
                        (Sections[0] as IDungeonItemSection).Accessibility = finalAccessibility;
                        (Sections[0] as IDungeonItemSection).Accessible = highestAccessible;
                    }
                    break;
                case AccessibilityLevel.SequenceBreak:
                case AccessibilityLevel.Normal:
                case AccessibilityLevel.Cleared:
                    {
                        (Sections[0] as IDungeonItemSection).Accessibility = finalAccessibility;
                        (Sections[0] as IDungeonItemSection).Accessible = Sections[0].Available;
                    }
                    break;
            }

            for (int i = 0; i < Bosses.Count; i++)
            {
                if (highestBossAccessibilities[i] == AccessibilityLevel.Normal &&
                    lowestBossAccessibilities[i] != AccessibilityLevel.Normal)
                {
                    highestBossAccessibilities[i] = AccessibilityLevel.SequenceBreak;
                }

                (Sections[i + 1] as IBossSection).Accessibility = highestBossAccessibilities[i];
            }
        }

        /// <summary>
        /// Updates the accessibility and number of accessible items for the contained sections
        /// using parallel operation.
        /// </summary>
        private void UpdateSectionAccessibilityParallel()
        {
            List<BlockingCollection<(List<KeyDoorID>, int, bool)>> keyDoorPermutationQueue =
                new List<BlockingCollection<(List<KeyDoorID>, int, bool)>>();
            List<Task[]> keyDoorTasks = new List<Task[]>();
            BlockingCollection<(List<KeyDoorID>, int, bool)> finalKeyDoorPermutationQueue =
                new BlockingCollection<(List<KeyDoorID>, int, bool)>();
            BlockingCollection<(List<AccessibilityLevel>, AccessibilityLevel, int)> resultQueue =
                new BlockingCollection<(List<AccessibilityLevel>, AccessibilityLevel, int)>();

            for (int i = 0; i <= SmallKeyDoors.Count; i++)
            {
                keyDoorPermutationQueue.Add(new BlockingCollection<(List<KeyDoorID>, int, bool)>());
            }

            List<int> smallKeyValues = GetSmallKeyValues();
            List<bool> bigKeyValues = GetBigKeyValues();

            foreach (int smallKeyValue in smallKeyValues)
            {
                foreach (bool bigKeyValue in bigKeyValues)
                {
                    keyDoorPermutationQueue[0].Add((new List<KeyDoorID>(), smallKeyValue, bigKeyValue));
                }
            }

            keyDoorPermutationQueue[0].CompleteAdding();

            for (int i = 0; i < keyDoorPermutationQueue.Count; i++)
            {
                int currentIteration = i;

                keyDoorTasks.Add(Enumerable.Range(1, Math.Max(1, Environment.ProcessorCount - 1))
                    .Select(_ => Task.Factory.StartNew(() =>
                    {
                        foreach (var item in keyDoorPermutationQueue[currentIteration].GetConsumingEnumerable())
                        {
                            IMutableDungeon dungeonData = GetDungeonData();

                            dungeonData.SetSmallKeyDoorState(item.Item1);
                            dungeonData.SetBigKeyDoorState(item.Item3);

                            int availableKeys = dungeonData.GetFreeKeys() + item.Item2 - item.Item1.Count;

                            if (availableKeys == 0)
                            {
                                finalKeyDoorPermutationQueue.Add(item);
                                DungeonDataQueue.Enqueue(dungeonData);
                                continue;
                            }

                            List<KeyDoorID> accessibleKeyDoors = dungeonData.GetAccessibleKeyDoors();

                            if (accessibleKeyDoors.Count == 0)
                            {
                                finalKeyDoorPermutationQueue.Add(item);
                                DungeonDataQueue.Enqueue(dungeonData);
                                continue;
                            }

                            foreach (KeyDoorID keyDoor in accessibleKeyDoors)
                            {
                                List<KeyDoorID> newPermutation = item.Item1.GetRange(0, item.Item1.Count);
                                newPermutation.Add(keyDoor);
                                keyDoorPermutationQueue[currentIteration + 1].Add((newPermutation, item.Item2,
                                    item.Item3));
                            }

                            DungeonDataQueue.Enqueue(dungeonData);
                        }
                    },
                    CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)).ToArray());
            }

            for (int i = 0; i < keyDoorTasks.Count; i++)
            {
                Task.WaitAll(keyDoorTasks[i]);
                keyDoorPermutationQueue[i].Dispose();

                if (i + 1 < keyDoorPermutationQueue.Count)
                {
                    keyDoorPermutationQueue[i + 1].CompleteAdding();
                }
                else
                {
                    finalKeyDoorPermutationQueue.CompleteAdding();
                }
            }

            Task[] finalKeyDoorTasks = Enumerable.Range(1, Math.Max(1, Environment.ProcessorCount - 1))
                .Select(_ => Task.Factory.StartNew(() =>
                {
                    foreach (var item in finalKeyDoorPermutationQueue.GetConsumingEnumerable())
                    {
                        IMutableDungeon dungeonData = GetDungeonData();

                        dungeonData.SetSmallKeyDoorState(item.Item1);
                        dungeonData.SetBigKeyDoorState(item.Item3);

                        if (!dungeonData.ValidateKeyLayout(item.Item2, item.Item3))
                        {
                            DungeonDataQueue.Enqueue(dungeonData);
                            continue;
                        }

                        List<AccessibilityLevel> bossAccessibility = dungeonData.GetBossAccessibility();

                        (AccessibilityLevel, int) accessibility =
                            dungeonData.GetItemAccessibility(item.Item2, item.Item3);

                        resultQueue.Add((bossAccessibility, accessibility.Item1, accessibility.Item2));

                        DungeonDataQueue.Enqueue(dungeonData);
                    }
                },
                CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)).ToArray();

            Task.WaitAll(finalKeyDoorTasks);
            finalKeyDoorPermutationQueue.Dispose();
            resultQueue.CompleteAdding();

            List<AccessibilityLevel> lowestBossAccessibilities = new List<AccessibilityLevel>();
            List<AccessibilityLevel> highestBossAccessibilities = new List<AccessibilityLevel>();

            for (int i = 0; i < Bosses.Count; i++)
            {
                lowestBossAccessibilities.Add(AccessibilityLevel.Normal);
                highestBossAccessibilities.Add(AccessibilityLevel.None);
            }

            AccessibilityLevel lowestAccessibility = AccessibilityLevel.Normal;
            AccessibilityLevel highestAccessibility = AccessibilityLevel.None;
            int highestAccessible = 0;

            foreach (var item in resultQueue.GetConsumingEnumerable())
            {
                for (int i = 0; i < item.Item1.Count; i++)
                {
                    if (item.Item1[i] < lowestBossAccessibilities[i])
                    {
                        lowestBossAccessibilities[i] = item.Item1[i];
                    }

                    if (item.Item1[i] > highestBossAccessibilities[i])
                    {
                        highestBossAccessibilities[i] = item.Item1[i];
                    }
                }

                if (item.Item2 < lowestAccessibility)
                {
                    lowestAccessibility = item.Item2;
                }

                if (item.Item2 > highestAccessibility)
                {
                    highestAccessibility = item.Item2;
                }

                if (item.Item3 > highestAccessible)
                {
                    highestAccessible = item.Item3;
                }
            }

            resultQueue.Dispose();

            AccessibilityLevel finalAccessibility = highestAccessibility;

            if (finalAccessibility == AccessibilityLevel.Normal &&
                lowestAccessibility < AccessibilityLevel.Normal)
            {
                finalAccessibility = AccessibilityLevel.SequenceBreak;
            }

            switch (finalAccessibility)
            {
                case AccessibilityLevel.None:
                    {
                        (Sections[0] as IDungeonItemSection).Accessibility = finalAccessibility;
                        (Sections[0] as IDungeonItemSection).Accessible = 0;
                    }
                    break;
                case AccessibilityLevel.Inspect:
                case AccessibilityLevel.Partial:
                    {
                        (Sections[0] as IDungeonItemSection).Accessibility = finalAccessibility;
                        (Sections[0] as IDungeonItemSection).Accessible = highestAccessible;
                    }
                    break;
                case AccessibilityLevel.SequenceBreak:
                case AccessibilityLevel.Normal:
                case AccessibilityLevel.Cleared:
                    {
                        (Sections[0] as IDungeonItemSection).Accessibility = finalAccessibility;
                        (Sections[0] as IDungeonItemSection).Accessible = Sections[0].Available;
                    }
                    break;
            }

            for (int i = 0; i < Bosses.Count; i++)
            {
                if (highestBossAccessibilities[i] == AccessibilityLevel.Normal &&
                    lowestBossAccessibilities[i] != AccessibilityLevel.Normal)
                {
                    highestBossAccessibilities[i] = AccessibilityLevel.SequenceBreak;
                }

                (Sections[i + 1] as IBossSection).Accessibility = highestBossAccessibilities[i];
            }
        }

        /// <summary>
        /// Returns the prize section of the dungeon.
        /// </summary>
        /// <returns>
        /// The prize section.
        /// </returns>
        public IPrizeSection GetPrizeSection()
        {
            if (ID == LocationID.GanonsTower)
            {
                return (IPrizeSection)Sections[4];
            }

            return (IPrizeSection)Sections[1];
        }
    }
}
