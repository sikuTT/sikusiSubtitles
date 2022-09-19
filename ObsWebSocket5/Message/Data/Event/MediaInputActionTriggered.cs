namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * An action has been performed on an input.

     * Event Subscription: MediaInputs
     */
    public class MediaInputActionTriggered : EventData {
        /** Name of the input */
        public string inputName;
        /** Action performed on the input. See `ObsMediaInputAction` enum */
        public string mediaAction;
    }
#pragma warning restore CS8618
}
