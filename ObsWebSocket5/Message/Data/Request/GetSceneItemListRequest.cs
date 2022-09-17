namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSceneItemListRequest : RequestData {
        public override string GetRequestType() { return "GetSceneItemList"; }
        public string sceneName;
    }
#pragma warning restore CS8618
}
