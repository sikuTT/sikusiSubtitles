namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSceneListResponse : ResponseData {
        /** Current program scene */
        public string currentProgramSceneName;
        /** Current preview scene. `null` if not in studio mode */
        public string currentPreviewSceneName;
        /** Array of scenes */
        public Scene[] scenes;

        public class Scene {
            public string sceneName;
            public long sceneIndex;
        }
    }
#pragma warning restore CS8618
}
