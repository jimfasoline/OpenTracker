﻿namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This is the enum type for dungeon node IDs.
    /// </summary>
    public enum DungeonNodeID
    {
        HCSanctuary,
        HCFront,
        HCPastEscapeFirstKeyDoor,
        HCPastEscapeSecondKeyDoor,
        HCDarkRoomFront,
        HCPastDarkCrossKeyDoor,
        HCPastSewerRatRoomKeyDoor,
        HCDarkRoomBack,
        HCBack,
        AT,
        ATDarkMaze,
        ATPastFirstKeyDoor,
        ATPastSecondKeyDoor,
        ATPastThirdKeyDoor,
        ATPastFourthKeyDoor,
        ATBossRoom,
        ATBoss,
        EP,
        EPBigChest,
        EPRightDarkRoom,
        EPPastRightKeyDoor,
        EPPastBigKeyDoor,
        EPBackDarkRoom,
        EPPastBackKeyDoor,
        EPBossRoom,
        EPBoss,
        DPFront,
        DPTorchItem,
        DPBigChest,
        DPPastRightKeyDoor,
        DPBack,
        DP2F,
        DP2FPastFirstKeyDoor,
        DP2FPastSecondKeyDoor,
        DPPastFourTorchWall,
        DPBossRoom,
        DPBoss,
        ToH,
        ToHPastKeyDoor,
        ToHBasementTorchRoom,
        ToHPastBigKeyDoor,
        ToHBigChest,
        ToHBoss,
        PoD,
        PoDPastFirstRedGoriyaRoom,
        PoDLobbyArena,
        PoDBigKeyChestArea,
        PoDPastCollapsingWalkwayKeyDoor,
        PoDDarkBasement,
        PoDHarmlessHellwayRoom,
        PoDPastDarkMazeKeyDoor,
        PoDDarkMaze,
        PoDBigChestLedge,
        PoDBigChest,
        PoDPastSecondRedGoriyaRoom,
        PoDPastBowStatue,
        PoDBossAreaDarkRooms,
        PoDPastHammerBlocks,
        PoDPastBossAreaKeyDoor,
        PoDBossRoom,
        PoDBoss,
        SP,
        SPAfterRiver,
        SPB1,
        SPB1PastFirstRightKeyDoor,
        SPB1PastSecondRightKeyDoor,
        SPB1PastRightHammerBlocks,
        SPB1KeyLedge,
        SPB1PastLeftKeyDoor,
        SPBigChest,
        SPB1Back,
        SPB1PastBackFirstKeyDoor,
        SPBossRoom,
        SPBoss,
        SWBigChestAreaBottom,
        SWBigChestAreaTop,
        SWBigChest,
        SWFrontLeftSide,
        SWFrontRightSide,
        SWFrontBackConnector,
        SWPastTheWorthlessKeyDoor,
        SWBack,
        SWBackPastFirstKeyDoor,
        SWBackPastFourTorchRoom,
        SWBackPastCurtains,
        SWBossRoom,
        SWBoss,
        TT,
        TTPastBigKeyDoor,
        TTPastFirstKeyDoor,
        TTPastSecondKeyDoor,
        TTPastBigChestRoomKeyDoor,
        TTPastHammerBlocks,
        TTBigChest,
        TTBossRoom,
        TTBoss,
        IP,
        IPPastEntranceFreezorRoom,
        IPB1LeftSide,
        IPB1RightSide,
        IPB2LeftSide,
        IPB2PastKeyDoor,
        IPB2PastHammerBlocks,
        IPB2PastLiftBlock,
        IPSpikeRoom,
        IPB4RightSide,
        IPB4IceRoom,
        IPB4FreezorRoom,
        IPFreezorChest,
        IPB4PastKeyDoor,
        IPBigChestArea,
        IPBigChest,
        IPB5,
        IPB5PastBigKeyDoor,
        IPB6,
        IPB6PastKeyDoor,
        IPB6PastHammerBlocks,
        IPB6PastLiftBlock,
        IPBoss,
        MM,
        MMPastEntranceGap,
        MMBigChest,
        MMB1TopSide,
        MMB1LobbyBeyondBlueBlocks,
        MMB1RightSideBeyondBlueBlocks,
        MMB1LeftSidePastFirstKeyDoor,
        MMB1LeftSidePastSecondKeyDoor,
        MMB1PastFourTorchRoom,
        MMF1PastFourTorchRoom,
        MMB1PastPortalBigKeyDoor,
        MMB1PastBridgeBigKeyDoor,
        MMDarkRoom,
        MMB2PastWorthlessKeyDoor,
        MMB2PastCaneOfSomariaSwitch,
        MMBossRoom,
        MMBoss,
        TRFront,
        TRF1CompassChestArea,
        TRF1FourTorchRoom,
        TRF1RollerRoom,
        TRF1FirstKeyDoorArea,
        TRF1PastFirstKeyDoor,
        TRF1PastSecondKeyDoor,
        TRB1,
        TRB1PastBigKeyChestKeyDoor,
        TRB1MiddleRightEntranceArea,
        TRB1BigChestArea,
        TRBigChest,
        TRB1RightSide,
        TRPastB1toB2KeyDoor,
        TRB2DarkRoomTop,
        TRB2DarkRoomBottom,
        TRB2PastDarkMaze,
        TRLaserBridgeChests,
        TRB2PastKeyDoor,
        TRB3,
        TRB3BossRoomEntry,
        TRBossRoom,
        TRBoss,
        GT,
        GTBobsTorch,
        GT1FLeft,
        GT1FLeftPastHammerBlocks,
        GT1FLeftDMsRoom,
        GT1FLeftPastBonkableGaps,
        GT1FLeftMapChestRoom,
        GT1FLeftSpikeTrapPortalRoom,
        GT1FLeftFiresnakeRoom,
        GT1FLeftPastFiresnakeRoomGap,
        GT1FLeftPastFiresnakeRoomKeyDoor,
        GT1FLeftRandomizerRoom,
        GT1FRight,
        GT1FRightTileRoom,
        GT1FRightFourTorchRoom,
        GT1FRightCompassRoom,
        GT1FRightPastCompassRoomPortal,
        GT1FRightCollapsingWalkway,
        GT1FBottomRoom,
        GTBoss1,
        GTB1BossChests,
        GTBigChest,
        GT3FPastRedGoriyaRooms,
        GT3FPastBigKeyDoor,
        GTBoss2,
        GT3FPastBoss2,
        GT5FPastFourTorchRooms,
        GT6FPastFirstKeyDoor,
        GT6FBossRoom,
        GTBoss3,
        GT6FPastBossRoomGap,
        GTFinalBossRoom,
        GTFinalBoss
    }
}