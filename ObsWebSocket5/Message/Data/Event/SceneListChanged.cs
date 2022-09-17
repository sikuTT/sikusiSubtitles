namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The list of scenes has changed.
     * 
     * TODO: Make OBS fire this event when scenes are reordered.

     * Event Subscription: Scenes
     */
    public class SceneListChanged : EventData {
        /** Updated array of scenes */
        object[] scenes;
    }
}
