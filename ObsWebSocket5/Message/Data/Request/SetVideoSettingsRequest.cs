namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetVideoSettingsRequest : RequestData {
        public override string GetRequestType() { return "SetVideoSettings"; }
        public long fpsNumerator;
        public long fpsDenominator;
        public long baseWidth;
        public long baseHeight;
        public long outputWidth;
        public long outputHeight;
    }
#pragma warning restore CS8618
}
