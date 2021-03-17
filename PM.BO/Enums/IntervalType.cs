namespace PM.BO.Enums
{
    public enum IntervalType
    {
        /// <summary>
        /// Time interval type
        /// </summary>
        Time = 0,

        /// <summary>
        /// Distance interval type
        /// </summary>
        Distance = 1,

        /// <summary>
        /// Rest interval type
        /// </summary>
        Rest = 2,

        /// <summary>
        /// Time undefined rest interval type
        /// </summary>
        TimeUndefinedRest = 3,

        /// <summary>
        /// Distance undefined rest interval type
        /// </summary>
        DistanceUndefinedRest = 4,

        /// <summary>
        /// Undefined rest interval type
        /// </summary>
        UndefinedRest = 5,

        /// <summary>
        /// Calorie interval type
        /// </summary>
        Calorie = 6,

        /// <summary>
        /// Calorie undefined rest interval type
        /// </summary>
        CalorieUndefinedRest = 7,

        /// <summary>
        /// Watt-minute interval type
        /// </summary>
        WattMinute = 8,

        /// <summary>
        /// Watt-minute undefined rest interval type
        /// </summary>
        WattMinuteUndefinedRest = 9,

        /// <summary>
        /// None
        /// </summary>
        None = 255
    }
}
