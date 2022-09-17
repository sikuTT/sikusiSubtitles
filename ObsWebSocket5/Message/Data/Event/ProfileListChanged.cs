namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The profile list has changed.

     * Event Subscription: Config
     */
    public class ProfileListChanged : EventData {
        /** Updated list of profiles */
        string[] profiles;
    }
}
