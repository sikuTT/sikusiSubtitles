namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
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
        public string inputName;
        /** New monitor type of the input */
        public string monitorType;
    }
#pragma warning restore CS8618
}
