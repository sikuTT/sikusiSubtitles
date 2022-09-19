namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetCurrentSceneTransitionSettingsRequest : RequestData {
        public override string GetRequestType() { return "SetCurrentSceneTransitionSettings"; }
        public object transitionSettings;
        public bool overlay;
    }
#pragma warning restore CS8618
}
