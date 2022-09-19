namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputSettingsRequest : RequestData {
        public override string GetRequestType() { return "GetInputSettings"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
