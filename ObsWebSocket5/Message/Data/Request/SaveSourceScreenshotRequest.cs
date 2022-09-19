namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SaveSourceScreenshotRequest : RequestData {
        public override string GetRequestType() { return "SaveSourceScreenshot"; }
        public string sourceName;
        public string imageFormat;
        public string imageFilePath;
        public long imageWidth;
        public long imageHeight;
        public long imageCompressionQuality;
    }
#pragma warning restore CS8618
}
