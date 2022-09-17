namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class CallVendorRequestRequest : RequestData {
        public override string GetRequestType() { return "CallVendorRequest"; }
        public string vendorName;
        public string requestType;
        public object requestData;
    }
#pragma warning restore CS8618
}
