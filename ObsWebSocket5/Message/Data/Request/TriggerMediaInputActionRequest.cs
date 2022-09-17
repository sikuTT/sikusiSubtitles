namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class TriggerMediaInputActionRequest : RequestData {
        public override string GetRequestType() { return "TriggerMediaInputAction"; }
        public string inputName;
        public string mediaAction;
    }
#pragma warning restore CS8618
}
