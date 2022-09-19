namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A scene item has been selected in the Ui.

     * Event Subscription: SceneItems
     */
    public class SceneItemSelected : EventData {
        /** Name of the scene the item is in */
        public string sceneName;
        /** Numeric ID of the scene item */
        public long sceneItemId;
    }
#pragma warning restore CS8618
}
