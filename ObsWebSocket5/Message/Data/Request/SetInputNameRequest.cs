namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetInputNameRequest : RequestData {
        public override string GetRequestType() { return "SetInputName"; }
        public string inputName;
        public string newInputName;
    }
#pragma warning restore CS8618
}
