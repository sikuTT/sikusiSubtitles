namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A scene's item list has been reindexed.

     * Event Subscription: SceneItems
     */
    public class SceneItemListReindexed : EventData {
        /** Name of the scene */
        public string sceneName;
        /** Array of scene item objects */
        public object[] sceneItems;
    }
#pragma warning restore CS8618
}
