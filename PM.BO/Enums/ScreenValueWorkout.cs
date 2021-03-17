namespace PM.BO.Enums
{
    public enum ScreenValueWorkout
    {
        SCREENVALUEWORKOUT_NONE, /** None value (0). */
        PrepareToRowWorkout = 1, /** Prepare to workout type (1). */
        SCREENVALUEWORKOUT_TERMINATEWORKOUT, /** Terminate workout type (2). */
        SCREENVALUEWORKOUT_REARMWORKOUT, /** Rearm workout type (3). */
        SCREENVALUEWORKOUT_REFRESHLOGCARD, /** Refresh local copies of logcard structures(4). */
        SCREENVALUEWORKOUT_PREPARETORACESTART, /** Prepare to race start (5). */
        SCREENVALUEWORKOUT_GOTOMAINSCREEN, /** Goto to main screen (6). */
        SCREENVALUEWORKOUT_LOGCARDBUSYWARNING, /** Log device busy warning (7). */
        SCREENVALUEWORKOUT_LOGCARDSELECTUSER, /** Log device select user (8). */
        SCREENVALUEWORKOUT_RESETRACEPARAMS, /** Reset race parameters (9). */
        SCREENVALUEWORKOUT_CABLETESTSLAVE, /** Cable test slave indication(10). */
        SCREENVALUEWORKOUT_FISHGAME, /** Fish game (11). */
        SCREENVALUEWORKOUT_DISPLAYPARTICIPANTINFO, /** Display participant info (12). */
        SCREENVALUEWORKOUT_DISPLAYPARTICIPANTINFOCONFIRM, /** Display participant info w/ confirmation
(13). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPETARGET = 20, /** Display type set to target (20). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPESTANDARD, /** Display type set to standard (21). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPEFORCEVELOCITY, /** Display type set to forcevelocity (22). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPEPACEBOAT, /** Display type set to Paceboat (23). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPEPERSTROKE, /** Display type set to perstroke (24). */
        SCREENVALUEWORKOUT_CHANGEDISPLAYTYPESIMPLE, /** Display type set to simple (25). */
        SCREENVALUEWORKOUT_CHANGEUNITSTYPETIMEMETERS = 30, /** Units type set to timemeters (30). */
        SCREENVALUEWORKOUT_CHANGEUNITSTYPEPACE, /** Units type set to pace (31). */
        SCREENVALUEWORKOUT_CHANGEUNITSTYPEWATTS, /** Units type set to watts (32). */
        SCREENVALUEWORKOUT_CHANGEUNITSTYPECALORICBURNRATE, /** Units type set to caloric burn rate(33). */
        SCREENVALUEWORKOUT_TARGETGAMEBASIC, /** Gasic target game (34). */
        SCREENVALUEWORKOUT_TARGETGAMEADVANCED, /** Advanced target game (35). */
        SCREENVALUEWORKOUT_DARTGAME, /** Dart game (36). */
        SCREENVALUEWORKOUT_GOTOUSBWAITREADY, /** USB wait ready (37). */
        SCREENVALUEWORKOUT_TACHCABLETESTDISABLE, /** Tach cable test disable (38). */
        SCREENVALUEWORKOUT_TACHSIMDISABLE, /** Tach simulator disable (39). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATE1, /** Tach simulator enable, rate = 1:12 (40). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATE2, /** Tach simulator enable, rate = 1:35 (41). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATE3, /** Tach simulator enable, rate = 1:42 (42). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATE4, /** Tach simulator enable, rate = 3:04 (43). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATE5, /** Tach simulator enable, rate = 3:14 (44). */
        SCREENVALUEWORKOUT_TACHCABLETESTENABLE, /** Tach cable test enable (45). */
        SCREENVALUEWORKOUT_CHANGEUNITSTYPECALORIES, /** Units type set to calories(46). */
        SCREENVALUEWORKOUT_VIRTUALKEY_A, /** Virtual key select A (47). */
        SCREENVALUEWORKOUT_VIRTUALKEY_B, /** Virtual key select B (48). */
        SCREENVALUEWORKOUT_VIRTUALKEY_C, /** Virtual key select C (49). */
        SCREENVALUEWORKOUT_VIRTUALKEY_D, /** Virtual key select D (50). */
        SCREENVALUEWORKOUT_VIRTUALKEY_E, /** Virtual key select E (51). */
        SCREENVALUEWORKOUT_VIRTUALKEY_UNITS, /** Virtual key select Units (52). */
        SCREENVALUEWORKOUT_VIRTUALKEY_DISPLAY, /** Virtual key select Display (53). */
        SCREENVALUEWORKOUT_VIRTUALKEY_MENU, /** Virtual key select Menu (54). */
        SCREENVALUEWORKOUT_TACHSIMENABLERATERANDOM, /** Tach simulator enable, rate = random(55). */
        SCREENVALUEWORKOUT_SCREENREDRAW = 255 /** Screen redraw (255). */
    }
}
