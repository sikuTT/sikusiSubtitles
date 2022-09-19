namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputKindListRequest : RequestData {
        public override string GetRequestType() { return "GetInputKindList"; }
        public bool unversioned;
    }
#pragma warning restore CS8618
}
