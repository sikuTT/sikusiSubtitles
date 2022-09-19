namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A new scene has been created.

     * Event Subscription: Scenes
     */
    public class SceneCreated : EventData {
        /** Name of the new scene */
        public string sceneName;
        /** Whether the new scene is a group */
        public bool isGroup;
    }
#pragma warning restore CS8618
}
