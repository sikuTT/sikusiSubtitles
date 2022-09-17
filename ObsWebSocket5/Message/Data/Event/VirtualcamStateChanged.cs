namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The state of the virtualcam output has changed.

     * Event Subscription: Outputs
     */
    public class VirtualcamStateChanged : EventData {
        /** Whether the output is active */
        bool outputActive;
        /** The specific state of the output */
        string outputState;
    }
}
