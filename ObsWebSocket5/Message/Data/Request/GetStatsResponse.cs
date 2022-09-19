namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetStatsResponse : ResponseData {
        /** Current CPU usage in percent */
        public long cpuUsage;
        /** Amount of memory in MB currently being used by OBS */
        public long memoryUsage;
        /** Available disk space on the device being used for recording storage */
        public long availableDiskSpace;
        /** Current FPS being rendered */
        public long activeFps;
        /** Average time in milliseconds that OBS is taking to render a frame */
        public long averageFrameRenderTime;
        /** Number of frames skipped by OBS in the render thread */
        public long renderSkippedFrames;
        /** Total number of frames outputted by the render thread */
        public long renderTotalFrames;
        /** Number of frames skipped by OBS in the output thread */
        public long outputSkippedFrames;
        /** Total number of frames outputted by the output thread */
        public long outputTotalFrames;
        /** Total number of messages received by obs-websocket from the client */
        public long webSocketSessionIncomingMessages;
        /** Total number of messages sent by obs-websocket to the client */
        public long webSocketSessionOutgoingMessages;
    }
#pragma warning restore CS8618
}
