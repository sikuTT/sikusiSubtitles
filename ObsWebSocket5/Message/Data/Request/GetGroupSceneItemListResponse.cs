using static ObsWebSocket5.Message.Data.Request.GetSceneItemListResponse;

namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetGroupSceneItemListResponse : ResponseData {
        /** Array of scene items in the group */
        public SceneItems[] sceneItems;
    }
#pragma warning restore CS8618
}
