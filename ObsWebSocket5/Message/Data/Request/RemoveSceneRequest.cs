namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class RemoveSceneRequest : RequestData {
        public override string GetRequestType() { return "RemoveScene"; }
        public string sceneName;
    }
#pragma warning restore CS8618
}
