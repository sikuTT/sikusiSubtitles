namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SleepRequest : RequestData {
        public override string GetRequestType() { return "Sleep"; }
        public long sleepMillis;
        public long sleepFrames;
    }
#pragma warning restore CS8618
}
