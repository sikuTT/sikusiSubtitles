namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A scene has been removed.

     * Event Subscription: Scenes
     */
    public class SceneRemoved : EventData {
        /** Name of the removed scene */
        public string sceneName;
        /** Whether the scene was a group */
        public bool isGroup;
    }
#pragma warning restore CS8618
}
