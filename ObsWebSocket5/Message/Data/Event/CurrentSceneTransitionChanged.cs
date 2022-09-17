namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The current scene transition has changed.

     * Event Subscription: Transitions
     */
    public class CurrentSceneTransitionChanged : EventData {
        /** Name of the new transition */
        string transitionName;
    }
}
