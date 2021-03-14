namespace PM.BO.Enums
{
    public enum PM3Command
    {
        // PM3-Specific Short Commands
        GET_WORKOUTTYPE = 0x81,
        GET_DRAGFACTOR = 0xC1,
        GET_STROKESTATE = 0xBF,
        GET_WORKTIME = 0xA0,
        GET_WORKDISTANCE = 0xA3,
        GET_ERRORVALUE = 0xC9,
        GET_WORKOUTSTATE = 0x8D,
        GET_WORKOUTINTERVALCOUNT = 0x9F,
        GET_INTERVALTYPE = 0x8E,
        GET_RESTTIME = 0xCF,
        GET_DISPLAYTYPE = 0x8A,
        GET_DISPLAYUNITS = 0x8B,

        // PM3-Specific Long Commands
        SET_SPLITDURATION = 0x05,
        GET_FORCEPLOTDATA = 0x6B,
        SET_SCREENERRORMODE = 0x27,
        GET_HEARTBEATDATA = 0x6C,
        GET_STROKESTATS = 0x6E
    }
}
