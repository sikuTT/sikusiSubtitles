namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The current scene transition has changed.

     * Event Subscription: Transitions
     */
    public class CurrentSceneTransitionChanged : EventData {
        /** Name of the new transition */
        public string transitionName;
    }
#pragma warning restore CS8618
}
