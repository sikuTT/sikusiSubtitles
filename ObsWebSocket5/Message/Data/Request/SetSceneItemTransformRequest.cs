namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSceneItemTransformRequest : RequestData {
        public override string GetRequestType() { return "SetSceneItemTransform"; }
        public string sceneName;
        public long sceneItemId;
        public object sceneItemTransform;
    }
#pragma warning restore CS8618
}
