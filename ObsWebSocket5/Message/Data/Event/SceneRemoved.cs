namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A scene has been removed.

     * Event Subscription: Scenes
     */
    public class SceneRemoved : EventData {
        /** Name of the removed scene */
        string sceneName;
        /** Whether the scene was a group */
        bool isGroup;
    }
}
