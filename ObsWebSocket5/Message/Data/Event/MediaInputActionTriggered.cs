namespace ObsWebSocket5.Message.Data.Event {
    /**
     * An action has been performed on an input.

     * Event Subscription: MediaInputs
     */
    public class MediaInputActionTriggered : EventData {
        /** Name of the input */
        string inputName;
        /** Action performed on the input. See `ObsMediaInputAction` enum */
        string mediaAction;
    }
}
