namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSceneCollectionListResponse : ResponseData {
        /** The name of the current scene collection */
        public string currentSceneCollectionName;
        /** Array of all available scene collections */
        public string[] sceneCollections;
    }
#pragma warning restore CS8618
}
