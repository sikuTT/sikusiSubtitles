namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * An input has been created.

     * Event Subscription: Inputs
     */
    public class InputCreated : EventData {
        /** Name of the input */
        public string inputName;
        /** The kind of the input */
        public string inputKind;
        /** The unversioned kind of input (aka no `_v2` stuff) */
        public string unversionedInputKind;
        /** The settings configured to the input when it was created */
        public object inputSettings;
        /** The default settings for the input */
        public object defaultInputSettings;
    }
#pragma warning restore CS8618
}
