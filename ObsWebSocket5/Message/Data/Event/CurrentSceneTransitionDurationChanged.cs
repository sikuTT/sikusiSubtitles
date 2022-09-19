namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The current scene transition duration has changed.

     * Event Subscription: Transitions
     */
    public class CurrentSceneTransitionDurationChanged : EventData {
        /** Transition duration in milliseconds */
        public long transitionDuration;
    }
#pragma warning restore CS8618
}
