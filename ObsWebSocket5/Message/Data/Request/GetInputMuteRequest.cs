namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputMuteRequest : RequestData {
        public override string GetRequestType() { return "GetInputMute"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
