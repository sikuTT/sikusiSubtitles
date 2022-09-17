namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputAudioSyncOffsetRequest : RequestData {
        public override string GetRequestType() { return "GetInputAudioSyncOffset"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
