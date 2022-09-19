namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetOutputSettingsRequest : RequestData {
        public override string GetRequestType() { return "GetOutputSettings"; }
        public string outputName;
    }
#pragma warning restore CS8618
}
