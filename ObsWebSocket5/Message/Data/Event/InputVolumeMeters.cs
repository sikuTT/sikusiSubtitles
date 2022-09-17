namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A high-volume event providing volume levels of all active inputs every 50 milliseconds.

     * Event Subscription: InputVolumeMeters
     */
    public class InputVolumeMeters : EventData {
        /** Array of active inputs with their associated volume levels */
        object[] inputs;
    }
}
