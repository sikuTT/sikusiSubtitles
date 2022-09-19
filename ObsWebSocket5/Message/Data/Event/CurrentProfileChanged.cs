namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The current profile has changed.

     * Event Subscription: Config
     */
    public class CurrentProfileChanged : EventData {
        /** Name of the new profile */
        public string profileName;
    }
#pragma warning restore CS8618
}
