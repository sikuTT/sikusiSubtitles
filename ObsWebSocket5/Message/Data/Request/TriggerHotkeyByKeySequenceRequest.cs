namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class TriggerHotkeyByKeySequenceRequest : RequestData {
        public override string GetRequestType() { return "TriggerHotkeyByKeySequence"; }
        public string keyId;
        public object keyModifiers;
    }
#pragma warning restore CS8618
}
