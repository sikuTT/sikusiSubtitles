namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class OpenVideoMixProjectorRequest : RequestData {
        public override string GetRequestType() { return "OpenVideoMixProjector"; }
        public string videoMixType;
        public long monitorIndex;
        public string projectorGeometry;
    }
#pragma warning restore CS8618
}
