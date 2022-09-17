namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The current profile has begun changing.

     * Event Subscription: Config
     */
    public class CurrentProfileChanging : EventData {
        /** Name of the current profile */
        string profileName;
    }
}
