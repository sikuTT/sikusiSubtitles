namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The current program scene has changed.

     * Event Subscription: Scenes
     */
    public class CurrentProgramSceneChanged : EventData {
        /** Name of the scene that was switched to */
        string sceneName;
    }
}
