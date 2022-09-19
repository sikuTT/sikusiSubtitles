namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetMediaInputStatusResponse : ResponseData {
        /** State of the media input */
        public string mediaState;
        /** Total duration of the playing media in milliseconds. `null` if not playing */
        public long mediaDuration;
        /** Position of the cursor in milliseconds. `null` if not playing */
        public long mediaCursor;
    }
#pragma warning restore CS8618
}
