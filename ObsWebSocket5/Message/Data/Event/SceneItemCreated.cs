namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A scene item has been created.

     * Event Subscription: SceneItems
     */
    public class SceneItemCreated : EventData {
        /** Name of the scene the item was added to */
        public string sceneName;
        /** Name of the underlying source (input/scene) */
        public string sourceName;
        /** Numeric ID of the scene item */
        public long sceneItemId;
        /** Index position of the item */
        public long sceneItemIndex;
    }
#pragma warning restore CS8618
}
