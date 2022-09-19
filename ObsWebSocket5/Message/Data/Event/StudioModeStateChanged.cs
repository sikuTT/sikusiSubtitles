namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * Studio mode has been enabled or disabled.

     * Event Subscription: Ui
     */
    public class StudioModeStateChanged : EventData {
        /** True == Enabled, False == Disabled */
        public bool studioModeEnabled;
    }
#pragma warning restore CS8618
}
