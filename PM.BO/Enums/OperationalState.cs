namespace PM.BO.Enums
{
    public enum OperationalState
    {
        /// <summary>
        /// Reset state
        /// </summary>
        Reset = 0,

        /// <summary>
        /// Ready state
        /// </summary>
        Ready = 1,

        /// <summary>
        /// Workout state
        /// </summary>
        Workout = 2,

        /// <summary>
        /// Warm-up state
        /// </summary>
        WarmUp = 3,

        /// <summary>
        /// Race state
        /// </summary>
        Race = 4,

        /// <summary>
        /// Power-off state
        /// </summary>
        PowerOff = 5,

        /// <summary>
        /// Pause state
        /// </summary>
        Pause = 6,

        /// <summary>
        /// Invoke boot loader state
        /// </summary>
        InvokeBootLoader = 7,

        /// <summary>
        /// Power-off ship state
        /// </summary>
        PowerOffShip = 8,

        /// <summary>
        /// Idle charge state
        /// </summary>
        IdleCharge = 9,

        /// <summary>
        /// Idle state
        /// </summary>
        Idle = 10,

        /// <summary>
        /// Manufacturing test state
        /// </summary>
        ManufacturingTest = 11,

        /// <summary>
        /// Firmware update state
        /// </summary>
        FirmwareUpdate = 12,

        /// <summary>
        /// Drag factor state
        /// </summary>
        DragFactor = 13,

        /// <summary>
        /// Drag factor calibration state
        /// </summary>
        DragFactorCalibration = 100
    }
}
