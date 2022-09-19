namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputDefaultSettingsRequest : RequestData {
        public override string GetRequestType() { return "GetInputDefaultSettings"; }
        public string inputKind;
    }
#pragma warning restore CS8618
}
