namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSceneItemIndexRequest : RequestData {
        public override string GetRequestType() { return "SetSceneItemIndex"; }
        public string sceneName;
        public long sceneItemId;
        public long sceneItemIndex;
    }
#pragma warning restore CS8618
}
