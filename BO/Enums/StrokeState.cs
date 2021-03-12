namespace BO.Enums
{
    /// <summary>
    /// Current stroke state
    /// </summary>
    /// <remarks>
    /// Catch would be the transition from recovery to driving. End-of-stroke would be the transition from driving to dwelling after drive
    /// </remarks>
    public enum StrokeState
    {
        WaitingForMinSpeed = 0,
        WaitingForAcceleration = 1,
        Driving = 2,
        Dwelling = 3,
        Recovery = 4
    }
}
