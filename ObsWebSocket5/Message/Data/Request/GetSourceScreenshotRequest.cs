namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSourceScreenshotRequest : RequestData {
        public override string GetRequestType() { return "GetSourceScreenshot"; }
        public string sourceName;
        public string imageFormat;
        public long imageWidth;
        public long imageHeight;
        public long imageCompressionQuality;
    }
#pragma warning restore CS8618
}
