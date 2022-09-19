namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The profile list has changed.

     * Event Subscription: Config
     */
    public class ProfileListChanged : EventData {
        /** Updated list of profiles */
        public string[] profiles;
    }
#pragma warning restore CS8618
}
