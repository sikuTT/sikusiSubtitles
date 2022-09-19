namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetRecordStatusResponse : ResponseData {
        /** Whether the output is active */
        public bool outputActive;
        /** Whether the output is paused */
        public bool ouputPaused;
        /** Current formatted timecode string for the output */
        public string outputTimecode;
        /** Current duration in milliseconds for the output */
        public long outputDuration;
        /** Number of bytes sent by the output */
        public long outputBytes;
    }
#pragma warning restore CS8618
}
