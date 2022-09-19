namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetOutputSettingsRequest : RequestData {
        public override string GetRequestType() { return "SetOutputSettings"; }
        public string outputName;
        public object outputSettings;
    }
#pragma warning restore CS8618
}
