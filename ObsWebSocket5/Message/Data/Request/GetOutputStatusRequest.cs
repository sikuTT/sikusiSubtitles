namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetOutputStatusRequest : RequestData {
        public override string GetRequestType() { return "GetOutputStatus"; }
        public string outputName;
    }
#pragma warning restore CS8618
}
