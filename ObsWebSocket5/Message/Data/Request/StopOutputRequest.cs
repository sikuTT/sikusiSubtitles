namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class StopOutputRequest : RequestData {
        public override string GetRequestType() { return "StopOutput"; }
        public string outputName;
    }
#pragma warning restore CS8618
}
