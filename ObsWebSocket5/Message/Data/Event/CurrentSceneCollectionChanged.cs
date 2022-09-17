namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The current scene collection has changed.
     * 
     * Note: If polling has been paused during `CurrentSceneCollectionChanging`, this is the que to restart polling.

     * Event Subscription: Config
     */
    public class CurrentSceneCollectionChanged : EventData {
        /** Name of the new scene collection */
        string sceneCollectionName;
    }
}
