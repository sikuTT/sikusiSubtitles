namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSceneItemEnabledRequest : RequestData {
        public override string GetRequestType() { return "GetSceneItemEnabled"; }
        public string sceneName;
        public long sceneItemId;
    }
#pragma warning restore CS8618
}
