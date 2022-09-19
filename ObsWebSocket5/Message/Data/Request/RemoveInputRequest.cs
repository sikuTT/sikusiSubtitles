namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class RemoveInputRequest : RequestData {
        public override string GetRequestType() { return "RemoveInput"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
