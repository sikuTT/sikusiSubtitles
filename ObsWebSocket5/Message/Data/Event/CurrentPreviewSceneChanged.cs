namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The current preview scene has changed.

     * Event Subscription: Scenes
     */
    public class CurrentPreviewSceneChanged : EventData {
        /** Name of the scene that was switched to */
        string sceneName;
    }
}
