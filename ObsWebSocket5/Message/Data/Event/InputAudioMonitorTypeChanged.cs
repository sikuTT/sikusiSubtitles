namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The monitor type of an input has changed.
     * 
     * Available types are:
     * 
     * - `OBS_MONITORING_TYPE_NONE`
     * - `OBS_MONITORING_TYPE_MONITOR_ONLY`
     * - `OBS_MONITORING_TYPE_MONITOR_AND_OUTPUT`

     * Event Subscription: Inputs
     */
    public class InputAudioMonitorTypeChanged : EventData {
        /** Name of the input */
        string inputName;
        /** New monitor type of the input */
        string monitorType;
    }
}
