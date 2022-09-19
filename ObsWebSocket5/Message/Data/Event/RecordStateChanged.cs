namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The state of the record output has changed.

     * Event Subscription: Outputs
     */
    public class RecordStateChanged : EventData {
        /** Whether the output is active */
        public bool outputActive;
        /** The specific state of the output */
        public string outputState;
        /** File name for the saved recording, if record stopped. `null` otherwise */
        public string outputPath;
    }
#pragma warning restore CS8618
}
