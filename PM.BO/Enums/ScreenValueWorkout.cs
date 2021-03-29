namespace PM.BO.Enums
{
    public enum ScreenValueWorkout
    {
        None = 0, /** None value (0). */
        PrepareToRowWorkout = 1, /** Prepare to workout type (1). */
        TerminateWorkout = 2, /** Terminate workout type (2). */
        RearmWorkout = 3, /** Rearm workout type (3). */
        RefreshLogCard = 4, /** Refresh local copies of logcard structures(4). */
        PrepareToRaceStart = 5, /** Prepare to race start (5). */
        GoToMainScreen = 6, /** Goto to main screen (6). */
        LogCardBusyWarning = 7, /** Log device busy warning (7). */
        LogCardSelectUser = 8, /** Log device select user (8). */
        ResetRaceParams = 9, /** Reset race parameters (9). */
        CableTestSlave = 10, /** Cable test slave indication(10). */
        FishGame = 11, /** Fish game (11). */
        DisplayParticipantInfo = 12, /** Display participant info (12). */
        DisplayParticipantInfoConfirm = 13, /** Display participant info w/ confirmation
(13). */
        ChangeDisplayTypeTarget = 20, /** Display type set to target (20). */
        ChangeDisplayTypeStandard = 21, /** Display type set to standard (21). */
        ChangeDisplayTypeForceVelocity = 22, /** Display type set to forcevelocity (22). */
        ChangeDisplayTypePaceBoat = 23, /** Display type set to Paceboat (23). */
        ChangeDisplayTypePerStroke = 24, /** Display type set to perstroke (24). */
        ChangeDisplayTypeSimple = 25, /** Display type set to simple (25). */
        ChangeUnitsTypeTimeMeters = 30, /** Units type set to timemeters (30). */
        ChangeUnitsTypePace = 31, /** Units type set to pace (31). */
        ChangeUnitsTypeWatts = 32, /** Units type set to watts (32). */
        ChangeUnitsTypeCaloricBurnRate = 33, /** Units type set to caloric burn rate(33). */
        TargetGameBasic = 34, /** Basic target game (34). */
        TargetGameAdvanced = 35, /** Advanced target game (35). */
        DartGame = 36, /** Dart game (36). */
        GoToUsbWaitReady = 37, /** USB wait ready (37). */
        TachCableTestDisable = 38, /** Tach cable test disable (38). */
        TachSimDisable = 39, /** Tach simulator disable (39). */
        TachSimEnableRate1 = 40, /** Tach simulator enable, rate = 1:12 (40). */
        TachSimEnableRate2 = 41, /** Tach simulator enable, rate = 1:35 (41). */
        TachSimEnableRate3 = 42, /** Tach simulator enable, rate = 1:42 (42). */
        TachSimEnableRate4 = 43, /** Tach simulator enable, rate = 3:04 (43). */
        TachSimEnableRate5 = 44, /** Tach simulator enable, rate = 3:14 (44). */
        TachCableTestEnable = 45, /** Tach cable test enable (45). */
        ChangeUnitsTypeCalories = 46, /** Units type set to calories(46). */
        VirtualKeyA = 47, /** Virtual key select A (47). */
        VirtualKeyB = 48, /** Virtual key select B (48). */
        VirtualKeyC = 49, /** Virtual key select C (49). */
        VirtualKeyD = 50, /** Virtual key select D (50). */
        VirtualKeyE = 51, /** Virtual key select E (51). */
        VirtualKeyUnits = 52, /** Virtual key select Units (52). */
        VirtualKeyDisplay = 53, /** Virtual key select Display (53). */
        VirtualKeyMenu = 54, /** Virtual key select Menu (54). */
        TachSimEnableRateRandom = 55, /** Tach simulator enable, rate = random(55). */
        ScreenRedraw = 255 /** Screen redraw (255). */
    }
}
