﻿using System.Collections.Generic;
using Autofac;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.Modes;
using OpenTracker.Models.Requirements;
using OpenTracker.Models.SaveLoad;
using Xunit;

namespace OpenTracker.UnitTests.Models.Requirements
{
    [Collection("Tests")]
    public class ModeRequirementTests
    {
        [Theory]
        [MemberData(nameof(ItemPlacement_Data))]
        [MemberData(nameof(SmallKeyShuffle_Data))]
        [MemberData(nameof(BigKeyShuffle_Data))]
        [MemberData(nameof(WorldState_Data))]
        [MemberData(nameof(EntranceShuffle_Data))]
        [MemberData(nameof(BossShuffle_Data))]
        [MemberData(nameof(EnemyShuffle_Data))]
        [MemberData(nameof(GuaranteedBossItems_Data))]
        [MemberData(nameof(GenericKeys_Data))]
        [MemberData(nameof(TakeAnyLocations_Data))]
        [MemberData(nameof(DungeonItemShuffle_Data))]
        [MemberData(nameof(WorldState_Entrance_Data))]
        [MemberData(nameof(TakeAny_Entrance_Data))]
        public void AccessibiltyTests(
            ModeSaveData modeData, RequirementType type, AccessibilityLevel expected)
        {
            var container = ContainerConfig.Configure();

            using var scope = container.BeginLifetimeScope();
            var mode = scope.Resolve<IMode>();
            var requirements = scope.Resolve<IRequirementDictionary>();
            mode.Load(modeData);
    
            Assert.Equal(expected, requirements[type].Accessibility);
        }
    
        public static IEnumerable<object[]> ItemPlacement_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    RequirementType.ItemPlacementBasic,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    RequirementType.ItemPlacementBasic,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Basic
                    },
                    RequirementType.ItemPlacementAdvanced,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        ItemPlacement = ItemPlacement.Advanced
                    },
                    RequirementType.ItemPlacementAdvanced,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> SmallKeyShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false
                    },
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true
                    },
                    RequirementType.SmallKeyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false
                    },
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true
                    },
                    RequirementType.SmallKeyShuffleOn,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BigKeyShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        BigKeyShuffle = false
                    },
                    RequirementType.BigKeyShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        BigKeyShuffle = true
                    },
                    RequirementType.BigKeyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        BigKeyShuffle = false
                    },
                    RequirementType.BigKeyShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        BigKeyShuffle = true
                    },
                    RequirementType.BigKeyShuffleOn,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> WorldState_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    RequirementType.WorldStateStandardOpen,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    RequirementType.WorldStateStandardOpen,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen
                    },
                    RequirementType.WorldStateInverted,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted
                    },
                    RequirementType.WorldStateInverted,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EntranceShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.EntranceShuffleNone,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.EntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.EntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Insanity
                    },
                    RequirementType.EntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.EntranceShuffleDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.EntranceShuffleDungeon,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.EntranceShuffleDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Insanity
                    },
                    RequirementType.EntranceShuffleDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.EntranceShuffleAll,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.EntranceShuffleAll,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.EntranceShuffleAll,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Insanity
                    },
                    RequirementType.EntranceShuffleAll,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.EntranceShuffleInsanity,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.EntranceShuffleInsanity,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.EntranceShuffleInsanity,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EntranceShuffle = EntranceShuffle.Insanity
                    },
                    RequirementType.EntranceShuffleInsanity,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> BossShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        BossShuffle = false
                    },
                    RequirementType.BossShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        BossShuffle = true
                    },
                    RequirementType.BossShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        BossShuffle = false
                    },
                    RequirementType.BossShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        BossShuffle = true
                    },
                    RequirementType.BossShuffleOn,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> EnemyShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    RequirementType.EnemyShuffleOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = true
                    },
                    RequirementType.EnemyShuffleOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = false
                    },
                    RequirementType.EnemyShuffleOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        EnemyShuffle = true
                    },
                    RequirementType.EnemyShuffleOn,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> GuaranteedBossItems_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        GuaranteedBossItems = false
                    },
                    RequirementType.GuaranteedBossItemsOff,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        GuaranteedBossItems = true
                    },
                    RequirementType.GuaranteedBossItemsOff,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        GuaranteedBossItems = false
                    },
                    RequirementType.GuaranteedBossItemsOn,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        GuaranteedBossItems = true
                    },
                    RequirementType.GuaranteedBossItemsOn,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> GenericKeys_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        GenericKeys = false
                    },
                    RequirementType.GenericKeys,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        GenericKeys = true
                    },
                    RequirementType.GenericKeys,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> TakeAnyLocations_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        TakeAnyLocations = false
                    },
                    RequirementType.TakeAnyLocations,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        TakeAnyLocations = true
                    },
                    RequirementType.TakeAnyLocations,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> DungeonItemShuffle_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        BigKeyShuffle = false
                    },
                    RequirementType.NoKeyShuffle,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        BigKeyShuffle = false
                    },
                    RequirementType.NoKeyShuffle,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        BigKeyShuffle = true
                    },
                    RequirementType.NoKeyShuffle,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        BigKeyShuffle = true
                    },
                    RequirementType.NoKeyShuffle,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        BigKeyShuffle = false
                    },
                    RequirementType.SmallKeyShuffleOnly,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        BigKeyShuffle = false
                    },
                    RequirementType.SmallKeyShuffleOnly,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        BigKeyShuffle = true
                    },
                    RequirementType.SmallKeyShuffleOnly,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        BigKeyShuffle = true
                    },
                    RequirementType.SmallKeyShuffleOnly,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        BigKeyShuffle = false
                    },
                    RequirementType.BigKeyShuffleOnly,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        BigKeyShuffle = false
                    },
                    RequirementType.BigKeyShuffleOnly,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        BigKeyShuffle = true
                    },
                    RequirementType.BigKeyShuffleOnly,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        BigKeyShuffle = true
                    },
                    RequirementType.BigKeyShuffleOnly,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        BigKeyShuffle = false
                    },
                    RequirementType.AllKeyShuffle,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        BigKeyShuffle = false
                    },
                    RequirementType.AllKeyShuffle,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = false,
                        BigKeyShuffle = true
                    },
                    RequirementType.AllKeyShuffle,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        SmallKeyShuffle = true,
                        BigKeyShuffle = true
                    },
                    RequirementType.AllKeyShuffle,
                    AccessibilityLevel.Normal
                }
            };
    
        public static IEnumerable<object[]> WorldState_Entrance_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNone,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.WorldStateStandardOpenEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNone,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNone,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.StandardOpen,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        WorldState = WorldState.Inverted,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.WorldStateInvertedEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                }
            };
    
        public static IEnumerable<object[]> TakeAny_Entrance_Data =>
            new List<object[]>
            {
                new object[]
                {
                    new ModeSaveData()
                    {
                        TakeAnyLocations = false,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        TakeAnyLocations = true,
                        EntranceShuffle = EntranceShuffle.None
                    },
                    RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        TakeAnyLocations = false,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        TakeAnyLocations = true,
                        EntranceShuffle = EntranceShuffle.Dungeon
                    },
                    RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon,
                    AccessibilityLevel.Normal
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        TakeAnyLocations = false,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                },
                new object[]
                {
                    new ModeSaveData()
                    {
                        TakeAnyLocations = true,
                        EntranceShuffle = EntranceShuffle.All
                    },
                    RequirementType.TakeAnyLocationsEntranceShuffleNoneDungeon,
                    AccessibilityLevel.None
                }
            };
    }
}
