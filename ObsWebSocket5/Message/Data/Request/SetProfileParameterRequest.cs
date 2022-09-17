namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetProfileParameterRequest : RequestData {
        public override string GetRequestType() { return "SetProfileParameter"; }
        public string parameterCategory;
        public string parameterName;
        public string parameterValue;
    }
#pragma warning restore CS8618
}
