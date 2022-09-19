namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class ToggleOutputRequest : RequestData {
        public override string GetRequestType() { return "ToggleOutput"; }
        public string outputName;
    }
#pragma warning restore CS8618
}
