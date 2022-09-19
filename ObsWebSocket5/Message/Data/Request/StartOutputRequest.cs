namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class StartOutputRequest : RequestData {
        public override string GetRequestType() { return "StartOutput"; }
        public string outputName;
    }
#pragma warning restore CS8618
}
