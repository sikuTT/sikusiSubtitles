namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class OpenInputPropertiesDialogRequest : RequestData {
        public override string GetRequestType() { return "OpenInputPropertiesDialog"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
