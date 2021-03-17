namespace PM.BO.Enums
{
    public enum Status
    {
        STATUSTYPE_NONE, /** None (0). */
        STATUSTYPE_BATTERY_LEVEL1_WARNING, /** Battery level 1 warning, status value = (current battery
 level/max battery value) * 100 (1). */
        STATUSTYPE_BATTERY_LEVEL2_WARNING, /** Battery level 2 warning, status value = (current battery
 level/max battery value) * 100 (2). */
        STATUSTYPE_LOGDEVICE_STATE, /** Log device state, status value = log device status (3). */
        STATUSTYPE_LOGCARD_STATE = STATUSTYPE_LOGDEVICE_STATE, /** Log device state, status value = log
 device status (3). */
        STATUSTYPE_POWERSOURCE_STATE, /** Power source, status value = power source status (4). */
        STATUSTYPE_LOGCARD_WORKOUTLOGGED_STATUS, /** Log device workout logged, status value = workout
 logged status (5). */
        STATUSTYPE_FLYWHEEL_STATE, /** Flywheel, status value = not turning, turning (6). */
        STATUSTYPE_BAD_UTILITY_STATE, /** Bad utility, status value = correct utilty, wrong utility (7). */
        STATUSTYPE_FWUPDATE_STATUS, /** Firmware update, status value = no update pending, update
 pending, update complete (8). */
        STATUSTYPE_UNSUPPORTEDUSBHOSTDEVICE, /** Unsupported USB host device, status value = unused (9). */
        STATUSTYPE_USBDRIVE_STATE, /** USB host drive, status value = uninitialized, initialized (10). */
        STATUSTYPE_LOADCONTROL_STATUS, /** Load control, status value = all loads allowed, usb host not
 allowed, backlight not allowed, neither allowed (11). */
        STATUSTYPE_USBLOGBOOK_STATUS, /** USB log book, status value = directory missing/corrupt, file
 missing/corrupt, validated (12). */
        STATUSTYPE_LOGSTORAGECAPACTYWARNING_STATUS, /** Log storage capacity warning, status value = current
 used capacity (13). */
        STATUSTYPE_FACTORYCALIBRATION_WARNING, /** Full calibration warning, status value = unused (14). */
        STATUSTYPE_VERIFYCALIBRATION_WARNING, /** Verify calibration warning, status value = unused (15). */
        STATUSTYPE_SERVICECALIBRATION_WARNING, /** Service calibration warning, status value = unused (16). */
    }
}
