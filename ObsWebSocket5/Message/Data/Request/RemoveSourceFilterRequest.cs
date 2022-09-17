namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class RemoveSourceFilterRequest : RequestData {
        public override string GetRequestType() { return "RemoveSourceFilter"; }
        public string sourceName;
        public string filterName;
    }
#pragma warning restore CS8618
}
