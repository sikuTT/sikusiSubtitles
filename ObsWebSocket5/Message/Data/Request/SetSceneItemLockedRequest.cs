namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSceneItemLockedRequest : RequestData {
        public override string GetRequestType() { return "SetSceneItemLocked"; }
        public string sceneName;
        public long sceneItemId;
        public bool sceneItemLocked;
    }
#pragma warning restore CS8618
}
