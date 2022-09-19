namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The state of the virtualcam output has changed.

     * Event Subscription: Outputs
     */
    public class VirtualcamStateChanged : EventData {
        /** Whether the output is active */
        public bool outputActive;
        /** The specific state of the output */
        public string outputState;
    }
#pragma warning restore CS8618
}
