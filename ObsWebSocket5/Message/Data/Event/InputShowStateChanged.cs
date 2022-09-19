namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * An input's show state has changed.
     * 
     * When an input is showing, it means it's being shown by the preview or a dialog.

     * Event Subscription: InputShowStateChanged
     */
    public class InputShowStateChanged : EventData {
        /** Name of the input */
        public string inputName;
        /** Whether the input is showing */
        public bool videoShowing;
    }
#pragma warning restore CS8618
}
