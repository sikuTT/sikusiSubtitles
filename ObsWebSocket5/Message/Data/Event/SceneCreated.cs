namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A new scene has been created.

     * Event Subscription: Scenes
     */
    public class SceneCreated : EventData {
        /** Name of the new scene */
        string sceneName;
        /** Whether the new scene is a group */
        bool isGroup;
    }
}
