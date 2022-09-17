namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A scene item has been created.

     * Event Subscription: SceneItems
     */
    public class SceneItemCreated : EventData {
        /** Name of the scene the item was added to */
        string sceneName;
        /** Name of the underlying source (input/scene) */
        string sourceName;
        /** Numeric ID of the scene item */
        long sceneItemId;
        /** Index position of the item */
        long sceneItemIndex;
    }
}
