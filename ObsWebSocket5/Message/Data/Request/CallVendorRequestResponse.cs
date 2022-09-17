namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class CallVendorRequestResponse : ResponseData {
        /** Echoed of `vendorName` */
        public string vendorName;
        /** Echoed of `requestType` */
        public string requestType;
        /** Object containing appropriate response data. {} if request does not provide any response data */
        public object responseData;
    }
#pragma warning restore CS8618
}
