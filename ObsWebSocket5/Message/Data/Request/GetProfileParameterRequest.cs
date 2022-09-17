namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetProfileParameterRequest : RequestData {
        public override string GetRequestType() { return "GetProfileParameter"; }
        public string parameterCategory;
        public string parameterName;
    }
#pragma warning restore CS8618
}
