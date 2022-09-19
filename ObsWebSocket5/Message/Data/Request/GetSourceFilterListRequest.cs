namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSourceFilterListRequest : RequestData {
        public override string GetRequestType() { return "GetSourceFilterList"; }
        public string sourceName;
    }
#pragma warning restore CS8618
}
