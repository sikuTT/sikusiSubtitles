namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class CreateSceneItemRequest : RequestData {
        public override string GetRequestType() { return "CreateSceneItem"; }
        public string sceneName;
        public string sourceName;
        public bool sceneItemEnabled;
    }
#pragma warning restore CS8618
}
