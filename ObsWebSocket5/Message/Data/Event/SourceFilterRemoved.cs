namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A filter has been removed from a source.

     * Event Subscription: Filters
     */
    public class SourceFilterRemoved : EventData {
        /** Name of the source the filter was on */
        public string sourceName;
        /** Name of the filter */
        public string filterName;
    }
#pragma warning restore CS8618
}
