namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The state of the replay buffer output has changed.

     * Event Subscription: Outputs
     */
    public class ReplayBufferStateChanged : EventData {
        /** Whether the output is active */
        public bool outputActive;
        /** The specific state of the output */
        public string outputState;
    }
#pragma warning restore CS8618
}
