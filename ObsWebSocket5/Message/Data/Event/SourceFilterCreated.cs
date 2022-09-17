namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A filter has been added to a source.

     * Event Subscription: Filters
     */
    public class SourceFilterCreated : EventData {
        /** Name of the source the filter was added to */
        string sourceName;
        /** Name of the filter */
        string filterName;
        /** The kind of the filter */
        string filterKind;
        /** Index position of the filter */
        long filterIndex;
        /** The settings configured to the filter when it was created */
        object filterSettings;
        /** The default settings for the filter */
        object defaultFilterSettings;
    }
}
