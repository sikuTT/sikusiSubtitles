namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The state of the replay buffer output has changed.

     * Event Subscription: Outputs
     */
    public class ReplayBufferStateChanged : EventData {
        /** Whether the output is active */
        bool outputActive;
        /** The specific state of the output */
        string outputState;
    }
}
