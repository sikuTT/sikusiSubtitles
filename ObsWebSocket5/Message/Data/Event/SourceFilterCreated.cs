namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * A filter has been added to a source.

     * Event Subscription: Filters
     */
    public class SourceFilterCreated : EventData {
        /** Name of the source the filter was added to */
        public string sourceName;
        /** Name of the filter */
        public string filterName;
        /** The kind of the filter */
        public string filterKind;
        /** Index position of the filter */
        public long filterIndex;
        /** The settings configured to the filter when it was created */
        public object filterSettings;
        /** The default settings for the filter */
        public object defaultFilterSettings;
    }
#pragma warning restore CS8618
}
