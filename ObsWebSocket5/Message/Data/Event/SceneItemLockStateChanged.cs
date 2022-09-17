namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A scene item's lock state has changed.

     * Event Subscription: SceneItems
     */
    public class SceneItemLockStateChanged : EventData {
        /** Name of the scene the item is in */
        string sceneName;
        /** Numeric ID of the scene item */
        long sceneItemId;
        /** Whether the scene item is locked */
        bool sceneItemLocked;
    }
}
