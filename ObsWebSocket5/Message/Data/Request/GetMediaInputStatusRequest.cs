namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetMediaInputStatusRequest : RequestData {
        public override string GetRequestType() { return "GetMediaInputStatus"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
