namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSourceFilterIndexRequest : RequestData {
        public override string GetRequestType() { return "SetSourceFilterIndex"; }
        public string sourceName;
        public string filterName;
        public long filterIndex;
    }
#pragma warning restore CS8618
}
