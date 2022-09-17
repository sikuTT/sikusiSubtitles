namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A scene item has been removed.
     * 
     * This event is not emitted when the scene the item is in is removed.

     * Event Subscription: SceneItems
     */
    public class SceneItemRemoved : EventData {
        /** Name of the scene the item was removed from */
        string sceneName;
        /** Name of the underlying source (input/scene) */
        string sourceName;
        /** Numeric ID of the scene item */
        long sceneItemId;
    }
}
