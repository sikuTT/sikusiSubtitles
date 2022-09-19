namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A source's filter list has been reindexed.

     * Event Subscription: Filters
     */
    public class SourceFilterListReindexed : EventData {
        /** Name of the source */
        public string sourceName;
        /** Array of filter objects */
        public object[] filters;
    }
#pragma warning restore CS8618
}
