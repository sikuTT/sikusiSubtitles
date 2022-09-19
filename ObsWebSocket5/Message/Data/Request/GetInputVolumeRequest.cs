namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputVolumeRequest : RequestData {
        public override string GetRequestType() { return "GetInputVolume"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
