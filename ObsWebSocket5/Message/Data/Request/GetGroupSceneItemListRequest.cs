namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetGroupSceneItemListRequest : RequestData {
        public override string GetRequestType() { return "GetGroupSceneItemList"; }
        public string sceneName;
    }
#pragma warning restore CS8618
}
