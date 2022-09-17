namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetProfileListResponse : ResponseData {
        /** The name of the current profile */
        public string currentProfileName;
        /** Array of all available profiles */
        public string[] profiles;
    }
#pragma warning restore CS8618
}
