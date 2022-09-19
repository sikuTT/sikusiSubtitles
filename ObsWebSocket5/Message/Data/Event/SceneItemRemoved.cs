namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A scene item has been removed.
     * 
     * This event is not emitted when the scene the item is in is removed.

     * Event Subscription: SceneItems
     */
    public class SceneItemRemoved : EventData {
        /** Name of the scene the item was removed from */
        public string sceneName;
        /** Name of the underlying source (input/scene) */
        public string sourceName;
        /** Numeric ID of the scene item */
        public long sceneItemId;
    }
#pragma warning restore CS8618
}
