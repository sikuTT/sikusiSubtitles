namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetPersistentDataRequest : RequestData {
        public override string GetRequestType() { return "SetPersistentData"; }
        public string realm;
        public string slotName;
        public object slotValue;
    }
#pragma warning restore CS8618
}
