namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetStreamServiceSettingsRequest : RequestData {
        public override string GetRequestType() { return "SetStreamServiceSettings"; }
        public string streamServiceType;
        public object streamServiceSettings;
    }
#pragma warning restore CS8618
}
