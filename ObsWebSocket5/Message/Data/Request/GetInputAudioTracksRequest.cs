namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputAudioTracksRequest : RequestData {
        public override string GetRequestType() { return "GetInputAudioTracks"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
