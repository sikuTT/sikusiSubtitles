namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class RemoveSceneItemRequest : RequestData {
        public override string GetRequestType() { return "RemoveSceneItem"; }
        public string sceneName;
        public long sceneItemId;
    }
#pragma warning restore CS8618
}
