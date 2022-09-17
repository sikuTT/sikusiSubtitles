namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetTBarPositionRequest : RequestData {
        public override string GetRequestType() { return "SetTBarPosition"; }
        public long position;
        public bool release;
    }
#pragma warning restore CS8618
}
