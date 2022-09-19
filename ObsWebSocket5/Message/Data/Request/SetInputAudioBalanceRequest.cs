namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetInputAudioBalanceRequest : RequestData {
        public override string GetRequestType() { return "SetInputAudioBalance"; }
        public string inputName;
        public long inputAudioBalance;
    }
#pragma warning restore CS8618
}
