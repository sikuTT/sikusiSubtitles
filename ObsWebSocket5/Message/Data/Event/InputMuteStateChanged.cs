namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * An input's mute state has changed.

     * Event Subscription: Inputs
     */
    public class InputMuteStateChanged : EventData {
        /** Name of the input */
        public string inputName;
        /** Whether the input is muted */
        public bool inputMuted;
    }
#pragma warning restore CS8618
}
