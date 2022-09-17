namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class CreateProfileRequest : RequestData {
        public override string GetRequestType() { return "CreateProfile"; }
        public string profileName;
    }
#pragma warning restore CS8618
}
