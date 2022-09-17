namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A source filter's enable state has changed.

     * Event Subscription: Filters
     */
    public class SourceFilterEnableStateChanged : EventData {
        /** Name of the source the filter is on */
        string sourceName;
        /** Name of the filter */
        string filterName;
        /** Whether the filter is enabled */
        bool filterEnabled;
    }
}
