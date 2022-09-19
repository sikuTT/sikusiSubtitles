namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetCurrentSceneCollectionRequest : RequestData {
        public override string GetRequestType() { return "SetCurrentSceneCollection"; }
        public string sceneCollectionName;
    }
#pragma warning restore CS8618
}
