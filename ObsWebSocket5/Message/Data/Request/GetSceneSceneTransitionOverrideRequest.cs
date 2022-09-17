namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSceneSceneTransitionOverrideRequest : RequestData {
        public override string GetRequestType() { return "GetSceneSceneTransitionOverride"; }
        public string sceneName;
    }
#pragma warning restore CS8618
}
