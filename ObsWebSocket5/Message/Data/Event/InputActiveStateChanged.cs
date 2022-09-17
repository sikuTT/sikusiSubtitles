namespace ObsWebSocket5.Message.Data.Event {
    /**
     * An input's active state has changed.
     * 
     * When an input is active, it means it's being shown by the program feed.

     * Event Subscription: InputActiveStateChanged
     */
    public class InputActiveStateChanged : EventData {
        /** Name of the input */
        string inputName;
        /** Whether the input is active */
        bool videoActive;
    }
}
