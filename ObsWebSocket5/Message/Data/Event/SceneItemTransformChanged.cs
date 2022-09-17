namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The transform/crop of a scene item has changed.

     * Event Subscription: SceneItemTransformChanged
     */
    public class SceneItemTransformChanged : EventData {
        /** The name of the scene the item is in */
        string sceneName;
        /** Numeric ID of the scene item */
        long sceneItemId;
        /** New transform/crop info of the scene item */
        object sceneItemTransform;
    }
}
