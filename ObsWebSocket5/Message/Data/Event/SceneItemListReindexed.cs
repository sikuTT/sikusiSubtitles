namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A scene's item list has been reindexed.

     * Event Subscription: SceneItems
     */
    public class SceneItemListReindexed : EventData {
        /** Name of the scene */
        string sceneName;
        /** Array of scene item objects */
        object[] sceneItems;
    }
}
