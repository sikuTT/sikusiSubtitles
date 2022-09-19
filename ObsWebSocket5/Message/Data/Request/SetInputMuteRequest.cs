namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetInputMuteRequest : RequestData {
        public override string GetRequestType() { return "SetInputMute"; }
        public string inputName;
        public bool inputMuted;
    }
#pragma warning restore CS8618
}
