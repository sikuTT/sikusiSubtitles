namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The state of the record output has changed.

     * Event Subscription: Outputs
     */
    public class RecordStateChanged : EventData {
        /** Whether the output is active */
        bool outputActive;
        /** The specific state of the output */
        string outputState;
        /** File name for the saved recording, if record stopped. `null` otherwise */
        string outputPath;
    }
}
