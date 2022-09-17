namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class DuplicateSceneItemRequest : RequestData {
        public override string GetRequestType() { return "DuplicateSceneItem"; }
        public string sceneName;
        public long sceneItemId;
        public string destinationSceneName;
    }
#pragma warning restore CS8618
}
