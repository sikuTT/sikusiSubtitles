namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetCurrentSceneTransitionRequest : RequestData {
        public override string GetRequestType() { return "SetCurrentSceneTransition"; }
        public string transitionName;
    }
#pragma warning restore CS8618
}
