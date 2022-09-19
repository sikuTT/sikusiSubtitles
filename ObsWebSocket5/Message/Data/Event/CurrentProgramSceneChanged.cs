namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The current program scene has changed.

     * Event Subscription: Scenes
     */
    public class CurrentProgramSceneChanged : EventData {
        /** Name of the scene that was switched to */
        public string sceneName;
    }
#pragma warning restore CS8618
}
