namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class OffsetMediaInputCursorRequest : RequestData {
        public override string GetRequestType() { return "OffsetMediaInputCursor"; }
        public string inputName;
        public long mediaCursorOffset;
    }
#pragma warning restore CS8618
}
