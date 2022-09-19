namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSceneItemEnabledRequest : RequestData {
        public override string GetRequestType() { return "SetSceneItemEnabled"; }
        public string sceneName;
        public long sceneItemId;
        public bool sceneItemEnabled;
    }
#pragma warning restore CS8618
}
