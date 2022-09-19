namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetPersistentDataRequest : RequestData {
        public override string GetRequestType() { return "GetPersistentData"; }
        public string realm;
        public string slotName;
    }
#pragma warning restore CS8618
}
