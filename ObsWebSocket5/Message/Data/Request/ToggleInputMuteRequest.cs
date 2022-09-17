namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class ToggleInputMuteRequest : RequestData {
        public override string GetRequestType() { return "ToggleInputMute"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
