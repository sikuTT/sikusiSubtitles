namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A scene item has been selected in the Ui.

     * Event Subscription: SceneItems
     */
    public class SceneItemSelected : EventData {
        /** Name of the scene the item is in */
        string sceneName;
        /** Numeric ID of the scene item */
        long sceneItemId;
    }
}
