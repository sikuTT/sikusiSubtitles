namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * An input's active state has changed.
     * 
     * When an input is active, it means it's being shown by the program feed.

     * Event Subscription: InputActiveStateChanged
     */
    public class InputActiveStateChanged : EventData {
        /** Name of the input */
        public string inputName;
        /** Whether the input is active */
        public bool videoActive;
    }
#pragma warning restore CS8618
}
