namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetCurrentProfileRequest : RequestData {
        public override string GetRequestType() { return "SetCurrentProfile"; }
        public string profileName;
    }
#pragma warning restore CS8618
}
