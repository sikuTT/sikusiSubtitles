namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSourceFilterEnabledRequest : RequestData {
        public override string GetRequestType() { return "SetSourceFilterEnabled"; }
        public string sourceName;
        public string filterName;
        public bool filterEnabled;
    }
#pragma warning restore CS8618
}
