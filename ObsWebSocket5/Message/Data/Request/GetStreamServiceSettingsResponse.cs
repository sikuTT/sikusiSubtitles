namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetStreamServiceSettingsResponse : ResponseData {
        /** Stream service type, like `rtmp_custom` or `rtmp_common` */
        public string streamServiceType;
        /** Stream service settings */
        public object streamServiceSettings;
    }
#pragma warning restore CS8618
}
