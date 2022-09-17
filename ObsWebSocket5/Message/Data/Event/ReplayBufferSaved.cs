namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The replay buffer has been saved.

     * Event Subscription: Outputs
     */
    public class ReplayBufferSaved : EventData {
        /** Path of the saved replay file */
        string savedReplayPath;
    }
}
