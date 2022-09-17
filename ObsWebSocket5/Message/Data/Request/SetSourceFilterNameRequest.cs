namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSourceFilterNameRequest : RequestData {
        public override string GetRequestType() { return "SetSourceFilterName"; }
        public string sourceName;
        public string filterName;
        public string newFilterName;
    }
#pragma warning restore CS8618
}
