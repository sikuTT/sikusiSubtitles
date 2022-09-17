namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The name of a scene has changed.

     * Event Subscription: Scenes
     */
    public class SceneNameChanged : EventData {
        /** Old name of the scene */
        string oldSceneName;
        /** New name of the scene */
        string sceneName;
    }
}
