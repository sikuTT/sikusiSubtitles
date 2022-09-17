namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The scene collection list has changed.

     * Event Subscription: Config
     */
    public class SceneCollectionListChanged : EventData {
        /** Updated list of scene collections */
        string[] sceneCollections;
    }
}
