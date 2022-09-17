namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetInputAudioBalanceRequest : RequestData {
        public override string GetRequestType() { return "GetInputAudioBalance"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
