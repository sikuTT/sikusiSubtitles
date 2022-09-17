namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetProfileParameterResponse : ResponseData {
        /** Value associated with the parameter. `null` if not set and no default */
        public string parameterValue;
        /** Default value associated with the parameter. `null` if no default */
        public string defaultParameterValue;
    }
#pragma warning restore CS8618
}
