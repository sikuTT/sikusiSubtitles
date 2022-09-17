namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class CreateSceneRequest : RequestData {
        public override string GetRequestType() { return "CreateScene"; }
        public string sceneName;
    }
#pragma warning restore CS8618
}
