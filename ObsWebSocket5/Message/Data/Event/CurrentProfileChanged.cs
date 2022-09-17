namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The current profile has changed.

     * Event Subscription: Config
     */
    public class CurrentProfileChanged : EventData {
        /** Name of the new profile */
        string profileName;
    }
}
