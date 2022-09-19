namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A scene item's lock state has changed.

     * Event Subscription: SceneItems
     */
    public class SceneItemLockStateChanged : EventData {
        /** Name of the scene the item is in */
        public string sceneName;
        /** Numeric ID of the scene item */
        public long sceneItemId;
        /** Whether the scene item is locked */
        public bool sceneItemLocked;
    }
#pragma warning restore CS8618
}
