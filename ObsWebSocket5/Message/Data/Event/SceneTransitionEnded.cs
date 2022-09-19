namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A scene transition has completed fully.
     * 
     * Note: Does not appear to trigger when the transition is interrupted by the user.

     * Event Subscription: Transitions
     */
    public class SceneTransitionEnded : EventData {
        /** Scene transition name */
        public string transitionName;
    }
#pragma warning restore CS8618
}
