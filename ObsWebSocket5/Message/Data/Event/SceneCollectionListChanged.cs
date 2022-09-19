namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The scene collection list has changed.

     * Event Subscription: Config
     */
    public class SceneCollectionListChanged : EventData {
        /** Updated list of scene collections */
        public string[] sceneCollections;
    }
#pragma warning restore CS8618
}
