namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputPropertiesListPropertyItemsRequest : RequestData {
        public override string GetRequestType() { return "GetInputPropertiesListPropertyItems"; }
        public string inputName;
        public string propertyName;
    }
#pragma warning restore CS8618
}
