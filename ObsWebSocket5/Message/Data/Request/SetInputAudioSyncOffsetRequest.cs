namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetInputAudioSyncOffsetRequest : RequestData {
        public override string GetRequestType() { return "SetInputAudioSyncOffset"; }
        public string inputName;
        public long inputAudioSyncOffset;
    }
#pragma warning restore CS8618
}
