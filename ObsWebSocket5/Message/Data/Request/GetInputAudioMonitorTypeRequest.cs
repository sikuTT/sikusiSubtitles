namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputAudioMonitorTypeRequest : RequestData {
        public override string GetRequestType() { return "GetInputAudioMonitorType"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
