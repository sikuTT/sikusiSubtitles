namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The sync offset of an input has changed.

     * Event Subscription: Inputs
     */
    public class InputAudioSyncOffsetChanged : EventData {
        /** Name of the input */
        string inputName;
        /** New sync offset in milliseconds */
        long inputAudioSyncOffset;
    }
}
