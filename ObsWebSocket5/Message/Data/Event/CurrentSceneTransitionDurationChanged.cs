namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The current scene transition duration has changed.

     * Event Subscription: Transitions
     */
    public class CurrentSceneTransitionDurationChanged : EventData {
        /** Transition duration in milliseconds */
        long transitionDuration;
    }
}
