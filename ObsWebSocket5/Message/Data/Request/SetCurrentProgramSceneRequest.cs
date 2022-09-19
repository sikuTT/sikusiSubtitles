namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class SetCurrentProgramSceneRequest : RequestData {
        public override string GetRequestType() { return "SetCurrentProgramScene"; }
        public string sceneName;
    }
#pragma warning restore CS8618
}
