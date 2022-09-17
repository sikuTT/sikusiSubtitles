namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSceneItemIndexRequest : RequestData {
        public override string GetRequestType() { return "GetSceneItemIndex"; }
        public string sceneName;
        public long sceneItemId;
    }
#pragma warning restore CS8618
}
