namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A source's filter list has been reindexed.

     * Event Subscription: Filters
     */
    public class SourceFilterListReindexed : EventData {
        /** Name of the source */
        string sourceName;
        /** Array of filter objects */
        object[] filters;
    }
}
