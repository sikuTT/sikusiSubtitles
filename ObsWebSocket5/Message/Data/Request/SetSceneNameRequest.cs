namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSceneNameRequest : RequestData {
        public override string GetRequestType() { return "SetSceneName"; }
        public string sceneName;
        public string newSceneName;
    }
#pragma warning restore CS8618
}
