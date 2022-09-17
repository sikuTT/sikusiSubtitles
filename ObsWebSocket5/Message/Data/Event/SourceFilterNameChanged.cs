namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The name of a source filter has changed.

     * Event Subscription: Filters
     */
    public class SourceFilterNameChanged : EventData {
        /** The source the filter is on */
        string sourceName;
        /** Old name of the filter */
        string oldFilterName;
        /** New name of the filter */
        string filterName;
    }
}
