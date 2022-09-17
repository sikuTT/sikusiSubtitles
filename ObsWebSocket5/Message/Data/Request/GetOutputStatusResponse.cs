namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetOutputStatusResponse : ResponseData {
        /** Whether the output is active */
        public bool outputActive;
        /** Whether the output is reconnecting */
        public bool outputReconnecting;
        /** Current formatted timecode string for the output */
        public string outputTimecode;
        /** Current duration in milliseconds for the output */
        public long outputDuration;
        /** Congestion of the output */
        public long outputCongestion;
        /** Number of bytes sent by the output */
        public long outputBytes;
        /** Number of frames skipped by the output's process */
        public long outputSkippedFrames;
        /** Total number of frames delivered by the output's process */
        public long outputTotalFrames;
    }
#pragma warning restore CS8618
}
