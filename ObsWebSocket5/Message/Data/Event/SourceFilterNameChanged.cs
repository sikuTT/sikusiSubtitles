namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The name of a source filter has changed.

     * Event Subscription: Filters
     */
    public class SourceFilterNameChanged : EventData {
        /** The source the filter is on */
        public string sourceName;
        /** Old name of the filter */
        public string oldFilterName;
        /** New name of the filter */
        public string filterName;
    }
#pragma warning restore CS8618
}
