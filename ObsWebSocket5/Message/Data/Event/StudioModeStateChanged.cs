namespace ObsWebSocket5.Message.Data.Event {
    /**
     * Studio mode has been enabled or disabled.

     * Event Subscription: Ui
     */
    public class StudioModeStateChanged : EventData {
        /** True == Enabled, False == Disabled */
        bool studioModeEnabled;
    }
}
