namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The current scene collection has begun changing.
     * 
     * Note: We recommend using this event to trigger a pause of all polling requests, as performing any requests during a
     * scene collection change is considered undefined behavior and can cause crashes!

     * Event Subscription: Config
     */
    public class CurrentSceneCollectionChanging : EventData {
        /** Name of the current scene collection */
        public string sceneCollectionName;
    }
#pragma warning restore CS8618
}
