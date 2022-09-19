namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The current scene collection has changed.
     * 
     * Note: If polling has been paused during `CurrentSceneCollectionChanging`, this is the que to restart polling.

     * Event Subscription: Config
     */
    public class CurrentSceneCollectionChanged : EventData {
        /** Name of the new scene collection */
        public string sceneCollectionName;
    }
#pragma warning restore CS8618
}
