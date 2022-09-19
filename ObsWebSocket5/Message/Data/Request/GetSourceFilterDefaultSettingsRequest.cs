namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSourceFilterDefaultSettingsRequest : RequestData {
        public override string GetRequestType() { return "GetSourceFilterDefaultSettings"; }
        public string filterKind;
    }
#pragma warning restore CS8618
}
