namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetStudioModeEnabledRequest : RequestData {
        public override string GetRequestType() { return "SetStudioModeEnabled"; }
        public bool studioModeEnabled;
    }
#pragma warning restore CS8618
}
