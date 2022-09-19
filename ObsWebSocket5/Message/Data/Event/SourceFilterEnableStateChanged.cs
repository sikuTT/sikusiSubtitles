namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A source filter's enable state has changed.

     * Event Subscription: Filters
     */
    public class SourceFilterEnableStateChanged : EventData {
        /** Name of the source the filter is on */
        public string sourceName;
        /** Name of the filter */
        public string filterName;
        /** Whether the filter is enabled */
        public bool filterEnabled;
    }
#pragma warning restore CS8618
}
