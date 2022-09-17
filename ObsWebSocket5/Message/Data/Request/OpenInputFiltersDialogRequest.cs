namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class OpenInputFiltersDialogRequest : RequestData {
        public override string GetRequestType() { return "OpenInputFiltersDialog"; }
        public string inputName;
    }
#pragma warning restore CS8618
}
