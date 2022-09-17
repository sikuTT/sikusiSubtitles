namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputListRequest : RequestData {
        public override string GetRequestType() { return "GetInputList"; }
        public string inputKind;
    }
#pragma warning restore CS8618
}
