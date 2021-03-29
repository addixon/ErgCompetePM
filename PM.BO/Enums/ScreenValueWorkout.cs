namespace PM.BO.Enums
{
    public enum ScreenValueWorkout
    {
        SCREENVALUEWORKOUT_NONE = 0, /** None value (0). */
        PrepareToRowWorkout = 1, /** Prepare to workout type (1). */
        TerminateWorkout = 2, /** Terminate workout type (2). */
        SCREENVALUEWORKOUT_REARMWORKOUT = 3, /** Rearm workout type (3). */
        SCREENVALUEWORKOUT_REFRESHLOGCARD = 4, /** Refresh local copies of logcard structures(4). */
        SCREENVALUEWORKOUT_PREPARETORACESTART = 5, /** Prepare to race start (5). */
        SCREENVALUEWORKOUT_GOTOMAINSCREEN = 6, /** Goto to main screen (6). */
        SCREENVALUEWORKOUT_LOGCARDBUSYWARNING = 7, /** Log device busy warning (7). */
        SCREENVALUEWORKOUT_LOGCARDSELECTUSER = 8, /** Log device select user (8). */
        SCREENVALUEWORKOUT_RESETRACEPARAMS = 9, /** Reset race parameters (9). */
        SCREENVALUEWORKOUT_CABLETESTSLAVE = 10, /** Cable test slave indication(10). */
        SCREENVALUEWORKOUT_FISHGAME = 11, /** Fish game (11). */
        SCREENVALUEWORKOUT_DISPLAYPARTICIPANTINFO = 12, /** Display participant info (12). */
        SCREENVALUEWORKOUT_DISPLAYPARTICIPANTINFOCONFIRM = 13, /** Display participant info w/ confirmation
(13). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPETARGET = 20, /** Display type set to target (20). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPESTANDARD = 21, /** Display type set to standard (21). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPEFORCEVELOCITY = 22, /** Display type set to forcevelocity (22). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPEPACEBOAT = 23, /** Display type set to Paceboat (23). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPEPERSTROKE = 24, /** Display type set to perstroke (24). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPESIMPLE = 25, /** Display type set to simple (25). */
        SCREENVALUEWORKOUT_CHANGEUNITSTYPETIMEMETERS = 30, /** Units type set to timemeters (30). */
        SCREENVALUEWORKOUT_CHANGEUNITSTYPEPACE = 31, /** Units type set to pace (31). */
        SCREENVALUEWORKOUT_CHANGEUNITSTYPEWATTS = 32, /** Units type set to watts (32). */
        SCREENVALUEWORKOUT_CHANGEUNITSTYPECALORICBURNRATE = 33, /** Units type set to caloric burn rate(33). */
        SCREENVALUEWORKOUT_TARGETGAMEBASIC = 34, /** Gasic target game (34). */
        SCREENVALUEWORKOUT_TARGETGAMEADVANCED = 35, /** Advanced target game (35). */
        SCREENVALUEWORKOUT_DARTGAME = 36, /** Dart game (36). */
        SCREENVALUEWORKOUT_GOTOUSBWAITREADY = 37, /** USB wait ready (37). */
        SCREENVALUEWORKOUT_TACHCABLETESTDISABLE = 38, /** Tach cable test disable (38). */
        SCREENVALUEWORKOUT_TACHSIMDISABLE = 39, /** Tach simulator disable (39). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATE1 = 40, /** Tach simulator enable, rate = 1:12 (40). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATE2 = 41, /** Tach simulator enable, rate = 1:35 (41). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATE3 = 42, /** Tach simulator enable, rate = 1:42 (42). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATE4 = 43, /** Tach simulator enable, rate = 3:04 (43). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATE5 = 44, /** Tach simulator enable, rate = 3:14 (44). */
        SCREENVALUEWORKOUT_TACHCABLETESTENABLE = 45, /** Tach cable test enable (45). */
        SCREENVALUEWORKOUT_CHANGEUNITSTYPECALORIES = 46, /** Units type set to calories(46). */
        SCREENVALUEWORKOUT_VIRTUALKEY_A = 47, /** Virtual key select A (47). */
        SCREENVALUEWORKOUT_VIRTUALKEY_B = 48, /** Virtual key select B (48). */
        SCREENVALUEWORKOUT_VIRTUALKEY_C = 49, /** Virtual key select C (49). */
        SCREENVALUEWORKOUT_VIRTUALKEY_D = 50, /** Virtual key select D (50). */
        SCREENVALUEWORKOUT_VIRTUALKEY_E = 51, /** Virtual key select E (51). */
        SCREENVALUEWORKOUT_VIRTUALKEY_UNITS = 52, /** Virtual key select Units (52). */
        SCREENVALUEWORKOUT_VIRTUALKEY_DISPLAY = 53, /** Virtual key select Display (53). */
        SCREENVALUEWORKOUT_VIRTUALKEY_MENU = 54, /** Virtual key select Menu (54). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATERANDOM = 55, /** Tach simulator enable, rate = random(55). */
        SCREENVALUEWORKOUT_SCREENREDRAW = 255 /** Screen redraw (255). */
    }
}
