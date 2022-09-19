namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class CreateSourceFilterRequest : RequestData {
        public override string GetRequestType() { return "CreateSourceFilter"; }
        public string sourceName;
        public string filterName;
        public string filterKind;
        public object filterSettings;
    }
#pragma warning restore CS8618
}
