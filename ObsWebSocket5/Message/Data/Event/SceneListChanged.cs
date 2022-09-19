namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The list of scenes has changed.
     * 
     * TODO: Make OBS fire this event when scenes are reordered.

     * Event Subscription: Scenes
     */
    public class SceneListChanged : EventData {
        /** Updated array of scenes */
        public object[] scenes;
    }
#pragma warning restore CS8618
}
