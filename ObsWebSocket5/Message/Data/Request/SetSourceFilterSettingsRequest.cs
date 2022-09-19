namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSourceFilterSettingsRequest : RequestData {
        public override string GetRequestType() { return "SetSourceFilterSettings"; }
        public string sourceName;
        public string filterName;
        public object filterSettings;
        public bool overlay;
    }
#pragma warning restore CS8618
}
