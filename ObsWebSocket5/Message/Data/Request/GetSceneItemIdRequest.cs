namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSceneItemIdRequest : RequestData {
        public override string GetRequestType() { return "GetSceneItemId"; }
        public string sceneName;
        public string sourceName;
        public long searchOffset;
    }
#pragma warning restore CS8618
}
