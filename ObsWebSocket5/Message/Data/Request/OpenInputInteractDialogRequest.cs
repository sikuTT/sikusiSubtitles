namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class OpenInputInteractDialogRequest : RequestData {
        public override string GetRequestType() { return "OpenInputInteractDialog"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
