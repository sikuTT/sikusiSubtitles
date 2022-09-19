namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SendStreamCaptionRequest : RequestData {
        public override string GetRequestType() { return "SendStreamCaption"; }
        public string captionText;
    }
#pragma warning restore CS8618
}
