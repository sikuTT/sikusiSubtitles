namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class TriggerHotkeyByNameRequest : RequestData {
        public override string GetRequestType() { return "TriggerHotkeyByName"; }
        public string hotkeyName;
    }
#pragma warning restore CS8618
}
