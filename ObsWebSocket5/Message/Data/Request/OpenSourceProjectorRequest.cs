namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class OpenSourceProjectorRequest : RequestData {
        public override string GetRequestType() { return "OpenSourceProjector"; }
        public string sourceName;
        public long monitorIndex;
        public string projectorGeometry;
    }
#pragma warning restore CS8618
}
