﻿namespace OpenTracker.Models.DungeonNodes
{
    /// <summary>
    /// This is the enum type for dungeon node IDs.
    /// </summary>
    public enum DungeonNodeID
    {
        HCSanctuary,
        HCFront,
        HCEscapeFirstKeyDoor,
        HCPastEscapeFirstKeyDoor,
        HCEscapeSecondKeyDoor,
        HCPastEscapeSecondKeyDoor,
        HCZeldasCellDoor,
        HCZeldasCell,
        HCDarkRoomFront,
        HCDarkCrossKeyDoor,
        HCPastDarkCrossKeyDoor,
        HCSewerRatRoomKeyDoor,
        HCPastSewerRatRoomKeyDoor,
        HCDarkRoomBack,
        HCBack,
        AT,
        ATDarkMaze,
        ATPastFirstKeyDoor,
        ATSecondKeyDoor,
        ATPastSecondKeyDoor,
        ATPastThirdKeyDoor,
        ATFourthKeyDoor,
        ATPastFourthKeyDoor,
        ATBossRoom,
        ATBoss,
        EP,
        EPBigChest,
        EPRightDarkRoom,
        EPRightKeyDoor,
        EPPastRightKeyDoor,
        EPBigKeyDoor,
        EPPastBigKeyDoor,
        EPBackDarkRoom,
        EPPastBackKeyDoor,
        EPBossRoom,
        EPBoss,
        DPFront,
        DPTorch,
        DPBigChest,
        DPRightKeyDoor,
        DPPastRightKeyDoor,
        DPBack,
        DP2F,
        DP2FFirstKeyDoor,
        DP2FPastFirstKeyDoor,
        DP2FSecondKeyDoor,
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
        PoDFrontKeyDoor,
        PoDLobbyArena,
        PoDBigKeyChestArea,
        PoDCollapsingWalkwayKeyDoor,
        PoDPastCollapsingWalkwayKeyDoor,
        PoDDarkBasement,
        PoDHarmlessHellwayKeyDoor,
        PoDHarmlessHellwayRoom,
        PoDDarkMazeKeyDoor,
        PoDPastDarkMazeKeyDoor,
        PoDDarkMaze,
        PoDBigChestLedge,
        PoDBigChest,
        PoDPastSecondRedGoriyaRoom,
        PoDPastBowStatue,
        PoDBossAreaDarkRooms,
        PoDPastHammerBlocks,
        PoDBossAreaKeyDoor,
        PoDPastBossAreaKeyDoor,
        PoDBossRoom,
        PoDBoss,
        SP,
        SPAfterRiver,
        SPB1,
        SPB1FirstRightKeyDoor,
        SPB1PastFirstRightKeyDoor,
        SPB1SecondRightKeyDoor,
        SPB1PastSecondRightKeyDoor,
        SPB1PastRightHammerBlocks,
        SPB1KeyLedge,
        SPB1LeftKeyDoor,
        SPB1PastLeftKeyDoor,
        SPBigChest,
        SPB1Back,
        SPB1BackFirstKeyDoor,
        SPB1PastBackFirstKeyDoor,
        SPBossRoomKeyDoor,
        SPBossRoom,
        SPBoss,
        SWBigChestAreaBottom,
        SWBigChestAreaTop,
        SWBigChest,
        SWFrontLeftKeyDoor,
        SWFrontLeftSide,
        SWFrontRightKeyDoor,
        SWFrontRightSide,
        SWFrontBackConnector,
        SWWorthlessKeyDoor,
        SWPastTheWorthlessKeyDoor,
        SWBack,
        SWBackFirstKeyDoor,
        SWBackPastFirstKeyDoor,
        SWBackPastFourTorchRoom,
        SWBackPastCurtains,
        SWBackSecondKeyDoor,
        SWBossRoom,
        SWBoss,
        TT,
        TTBigKeyDoor,
        TTPastBigKeyDoor,
        TTFirstKeyDoor,
        TTPastFirstKeyDoor,
        TTPastSecondKeyDoor,
        TTBigChestKeyDoor,
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
        IPB2KeyDoor,
        IPB2PastKeyDoor,
        IPB2PastHammerBlocks,
        IPB2PastLiftBlock,
        IPB3KeyDoor,
        IPSpikeRoom,
        IPB4RightSide,
        IPB4IceRoom,
        IPB4FreezorRoom,
        IPFreezorChest,
        IPB4KeyDoor,
        IPB4PastKeyDoor,
        IPBigChestArea,
        IPBigChest,
        IPB5,
        IPB5PastBigKeyDoor,
        IPB6,
        IPB6KeyDoor,
        IPB6PastKeyDoor,
        IPB6PreBossRoom,
        IPB6PastHammerBlocks,
        IPB6PastLiftBlock,
        IPBoss,
        MM,
        MMPastEntranceGap,
        MMBigChest,
        MMB1TopLeftKeyDoor,
        MMB1TopRightKeyDoor,
        MMB1TopSide,
        MMB1LobbyBeyondBlueBlocks,
        MMB1RightSideKeyDoor,
        MMB1RightSideBeyondBlueBlocks,
        MMB1LeftSideFirstKeyDoor,
        MMB1LeftSidePastFirstKeyDoor,
        MMB1LeftSideSecondKeyDoor,
        MMB1LeftSidePastSecondKeyDoor,
        MMB1PastFourTorchRoom,
        MMF1PastFourTorchRoom,
        MMB1PastPortalBigKeyDoor,
        MMBridgeBigKeyDoor,
        MMB1PastBridgeBigKeyDoor,
        MMDarkRoom,
        MMB2WorthlessKeyDoor, 
        MMB2PastWorthlessKeyDoor,
        MMB2PastCaneOfSomariaSwitch,
        MMBossRoom,
        MMBoss,
        TRFront,
        TRF1SomariaTrack,
        TRF1CompassChestArea,
        TRF1FourTorchRoom,
        TRF1RollerRoom,
        TRF1FirstKeyDoorArea,
        TRF1FirstKeyDoor,
        TRF1PastFirstKeyDoor,
        TRF1SecondKeyDoor,
        TRF1PastSecondKeyDoor,
        TRB1,
        TRB1BigKeyChestKeyDoor,
        TRB1PastBigKeyChestKeyDoor,
        TRB1MiddleRightEntranceArea,
        TRB1BigChestArea,
        TRBigChest,
        TRB1BigKeyDoor,
        TRB1RightSide,
        TRPastB1toB2KeyDoor,
        TRB2DarkRoomTop,
        TRB2DarkRoomBottom,
        TRB2PastDarkMaze,
        TRLaserBridgeChests,
        TRB2KeyDoor,
        TRB2PastKeyDoor,
        TRB3,
        TRB3BossRoomEntry,
        TRBossRoom,
        TRBoss,
        GT,
        GTBobsTorch,
        GT1FLeft,
        GT1FLeftToRightKeyDoor,
        GT1FLeftPastHammerBlocks,
        GT1FLeftDMsRoom,
        GT1FLeftPastBonkableGaps,
        GT1FMapChestRoomKeyDoor,
        GT1FLeftMapChestRoom,
        GT1FSpikeTrapPortalRoomKeyDoor,
        GT1FLeftSpikeTrapPortalRoom,
        GT1FLeftFiresnakeRoom,
        GT1FLeftPastFiresnakeRoomGap,
        GT1FFiresnakeRoomKeyDoor,
        GT1FLeftPastFiresnakeRoomKeyDoor,
        GT1FLeftRandomizerRoom,
        GT1FRight,
        GT1FRightTileRoom,
        GT1FTileRoomKeyDoor,
        GT1FRightFourTorchRoom,
        GT1FRightCompassRoom,
        GT1FRightPastCompassRoomPortal,
        GT1FCollapsingWalkwayKeyDoor,
        GT1FRightCollapsingWalkway,
        GT1FBottomRoom,
        GTBoss1,
        GTB1BossChests,
        GTBigChest,
        GT3FPastRedGoriyaRooms,
        GT3FBigKeyDoor,
        GT3FPastBigKeyDoor,
        GTBoss2,
        GT4FPastBoss2,
        GT5FPastFourTorchRooms,
        GT6FFirstKeyDoor,
        GT6FPastFirstKeyDoor,
        GT6FSecondKeyDoor,
        GT6FBossRoom,
        GTBoss3,
        GTBoss3Item,
        GT6FPastBossRoomGap,
        GTFinalBossRoom,
        GTFinalBoss
    }
}
