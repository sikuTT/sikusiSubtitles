namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A scene item's enable state has changed.

     * Event Subscription: SceneItems
     */
    public class SceneItemEnableStateChanged : EventData {
        /** Name of the scene the item is in */
        string sceneName;
        /** Numeric ID of the scene item */
        long sceneItemId;
        /** Whether the scene item is enabled (visible) */
        bool sceneItemEnabled;
    }
}
