namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class CreateInputRequest : RequestData {
        public override string GetRequestType() { return "CreateInput"; }
        public string sceneName;
        public string inputName;
        public string inputKind;
        public object inputSettings;
        public bool sceneItemEnabled;
    }
#pragma warning restore CS8618
}
