namespace ObsWebSocket5.Message.Data.Event {
    /**
     * An input has been created.

     * Event Subscription: Inputs
     */
    public class InputCreated : EventData {
        /** Name of the input */
        string inputName;
        /** The kind of the input */
        string inputKind;
        /** The unversioned kind of input (aka no `_v2` stuff) */
        string unversionedInputKind;
        /** The settings configured to the input when it was created */
        object inputSettings;
        /** The default settings for the input */
        object defaultInputSettings;
    }
}
