namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetInputAudioMonitorTypeRequest : RequestData {
        public override string GetRequestType() { return "SetInputAudioMonitorType"; }
        public string inputName;
        public string monitorType;
    }
#pragma warning restore CS8618
}
