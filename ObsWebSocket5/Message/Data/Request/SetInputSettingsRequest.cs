namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetInputSettingsRequest : RequestData {
        public override string GetRequestType() { return "SetInputSettings"; }
        public string inputName;
        public object inputSettings;
        public bool overlay;
    }
#pragma warning restore CS8618
}
