namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSourceFilterResponse : ResponseData {
        /** Whether the filter is enabled */
        public bool filterEnabled;
        /** Index of the filter in the list, beginning at 0 */
        public long filterIndex;
        /** The kind of filter */
        public string filterKind;
        /** Settings object associated with the filter */
        public object filterSettings;
    }
#pragma warning restore CS8618
}
