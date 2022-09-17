namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A filter has been removed from a source.

     * Event Subscription: Filters
     */
    public class SourceFilterRemoved : EventData {
        /** Name of the source the filter was on */
        string sourceName;
        /** Name of the filter */
        string filterName;
    }
}
