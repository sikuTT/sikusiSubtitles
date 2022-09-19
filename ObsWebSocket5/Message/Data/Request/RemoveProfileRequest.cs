namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class RemoveProfileRequest : RequestData {
        public override string GetRequestType() { return "RemoveProfile"; }
        public string profileName;
    }
#pragma warning restore CS8618
}
