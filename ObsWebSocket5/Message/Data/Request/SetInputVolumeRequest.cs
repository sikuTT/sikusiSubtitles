namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetInputVolumeRequest : RequestData {
        public override string GetRequestType() { return "SetInputVolume"; }
        public string inputName;
        public long inputVolumeMul;
        public long inputVolumeDb;
    }
#pragma warning restore CS8618
}
