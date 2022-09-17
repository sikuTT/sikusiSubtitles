namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetSceneSceneTransitionOverrideRequest : RequestData {
        public override string GetRequestType() { return "SetSceneSceneTransitionOverride"; }
        public string sceneName;
        public string transitionName;
        public long transitionDuration;
    }
#pragma warning restore CS8618
}
