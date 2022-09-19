namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A high-volume event providing volume levels of all active inputs every 50 milliseconds.

     * Event Subscription: InputVolumeMeters
     */
    public class InputVolumeMeters : EventData {
        /** Array of active inputs with their associated volume levels */
        public object[] inputs;
    }
#pragma warning restore CS8618
}
