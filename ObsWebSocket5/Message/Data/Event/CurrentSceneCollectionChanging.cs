namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The current scene collection has begun changing.
     * 
     * Note: We recommend using this event to trigger a pause of all polling requests, as performing any requests during a
     * scene collection change is considered undefined behavior and can cause crashes!

     * Event Subscription: Config
     */
    public class CurrentSceneCollectionChanging : EventData {
        /** Name of the current scene collection */
        string sceneCollectionName;
    }
}
