namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The name of an input has changed.

     * Event Subscription: Inputs
     */
    public class InputNameChanged : EventData {
        /** Old name of the input */
        public string oldInputName;
        /** New name of the input */
        public string inputName;
    }
#pragma warning restore CS8618
}
