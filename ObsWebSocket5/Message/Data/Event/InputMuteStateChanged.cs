namespace ObsWebSocket5.Message.Data.Event {
    /**
     * An input's mute state has changed.

     * Event Subscription: Inputs
     */
    public class InputMuteStateChanged : EventData {
        /** Name of the input */
        string inputName;
        /** Whether the input is muted */
        bool inputMuted;
    }
}
