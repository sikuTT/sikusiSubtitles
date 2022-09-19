namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The replay buffer has been saved.

     * Event Subscription: Outputs
     */
    public class ReplayBufferSaved : EventData {
        /** Path of the saved replay file */
        public string savedReplayPath;
    }
#pragma warning restore CS8618
}
