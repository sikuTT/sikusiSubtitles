namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The sync offset of an input has changed.

     * Event Subscription: Inputs
     */
    public class InputAudioSyncOffsetChanged : EventData {
        /** Name of the input */
        public string inputName;
        /** New sync offset in milliseconds */
        public long inputAudioSyncOffset;
    }
#pragma warning restore CS8618
}
