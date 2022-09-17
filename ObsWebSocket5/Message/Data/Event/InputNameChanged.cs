namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The name of an input has changed.

     * Event Subscription: Inputs
     */
    public class InputNameChanged : EventData {
        /** Old name of the input */
        string oldInputName;
        /** New name of the input */
        string inputName;
    }
}
