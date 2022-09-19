namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetMediaInputCursorRequest : RequestData {
        public override string GetRequestType() { return "SetMediaInputCursor"; }
        public string inputName;
        public long mediaCursor;
    }
#pragma warning restore CS8618
}
