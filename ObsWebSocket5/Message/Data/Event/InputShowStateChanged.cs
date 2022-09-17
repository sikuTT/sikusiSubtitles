namespace ObsWebSocket5.Message.Data.Event {
    /**
     * An input's show state has changed.
     * 
     * When an input is showing, it means it's being shown by the preview or a dialog.

     * Event Subscription: InputShowStateChanged
     */
    public class InputShowStateChanged : EventData {
        /** Name of the input */
        string inputName;
        /** Whether the input is showing */
        bool videoShowing;
    }
}
