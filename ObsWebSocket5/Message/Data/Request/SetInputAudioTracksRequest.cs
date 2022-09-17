namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetInputAudioTracksRequest : RequestData {
        public override string GetRequestType() { return "SetInputAudioTracks"; }
        public string inputName;
        public object inputAudioTracks;
    }
#pragma warning restore CS8618
}
