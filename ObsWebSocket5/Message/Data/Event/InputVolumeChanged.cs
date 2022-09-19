namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * An input's volume level has changed.

     * Event Subscription: Inputs
     */
    public class InputVolumeChanged : EventData {
        /** Name of the input */
        public string inputName;
        /** New volume level in multimap */
        public long inputVolumeMul;
        /** New volume level in dB */
        public long inputVolumeDb;
    }
#pragma warning restore CS8618
}
