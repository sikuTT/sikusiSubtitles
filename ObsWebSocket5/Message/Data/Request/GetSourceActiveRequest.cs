namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSourceActiveRequest : RequestData {
        public override string GetRequestType() { return "GetSourceActive"; }
        public string sourceName;
    }
#pragma warning restore CS8618
}
