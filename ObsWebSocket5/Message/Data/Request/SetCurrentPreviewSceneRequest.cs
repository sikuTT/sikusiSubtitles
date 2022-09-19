namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetCurrentPreviewSceneRequest : RequestData {
        public override string GetRequestType() { return "SetCurrentPreviewScene"; }
        public string sceneName;
    }
#pragma warning restore CS8618
}
