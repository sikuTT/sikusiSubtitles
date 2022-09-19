namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class CreateSceneCollectionRequest : RequestData {
        public override string GetRequestType() { return "CreateSceneCollection"; }
        public string sceneCollectionName;
    }
#pragma warning restore CS8618
}
