namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The name of a scene has changed.

     * Event Subscription: Scenes
     */
    public class SceneNameChanged : EventData {
        /** Old name of the scene */
        public string oldSceneName;
        /** New name of the scene */
        public string sceneName;
    }
#pragma warning restore CS8618
}
