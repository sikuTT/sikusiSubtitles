namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetVersionResponse : ResponseData {
        /** Current OBS Studio version */
        public string obsVersion;
        /** Current obs-websocket version */
        public string obsWebSocketVersion;
        /** Current latest obs-websocket RPC version */
        public long rpcVersion;
        /** Array of available RPC requests for the currently negotiated RPC version */
        public string[] availableRequests;
        /** Image formats available in `GetSourceScreenshot` and `SaveSourceScreenshot` requests. */
        public string[] supportedImageFormats;
        /** Name of the platform. Usually `windows`, `macos`, or `ubuntu` (linux flavor). Not guaranteed to be any of those */
        public string platform;
        /** Description of the platform, like `Windows 10 (10.0)` */
        public string platformDescription;
    }
#pragma warning restore CS8618
}
