namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class PressInputPropertiesButtonRequest : RequestData {
        public override string GetRequestType() { return "PressInputPropertiesButton"; }
        public string inputName;
        public string propertyName;
    }
#pragma warning restore CS8618
}
