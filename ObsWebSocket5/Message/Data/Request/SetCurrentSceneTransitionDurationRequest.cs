namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetCurrentSceneTransitionDurationRequest : RequestData {
        public override string GetRequestType() { return "SetCurrentSceneTransitionDuration"; }
        public long transitionDuration;
    }
#pragma warning restore CS8618
}
