namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The transform/crop of a scene item has changed.

     * Event Subscription: SceneItemTransformChanged
     */
    public class SceneItemTransformChanged : EventData {
        /** The name of the scene the item is in */
        public string sceneName;
        /** Numeric ID of the scene item */
        public long sceneItemId;
        /** New transform/crop info of the scene item */
        public object sceneItemTransform;
    }
#pragma warning restore CS8618
}
