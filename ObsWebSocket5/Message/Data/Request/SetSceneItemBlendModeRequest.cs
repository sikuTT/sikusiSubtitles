namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSceneItemBlendModeRequest : RequestData {
        public override string GetRequestType() { return "SetSceneItemBlendMode"; }
        public string sceneName;
        public long sceneItemId;
        public string sceneItemBlendMode;
    }
#pragma warning restore CS8618
}
