namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSceneTransitionListResponse : ResponseData {
        /** Name of the current scene transition. Can be null */
        public string currentSceneTransitionName;
        /** Kind of the current scene transition. Can be null */
        public string currentSceneTransitionKind;
        /** Array of transitions */
        public object[] transitions;
    }
#pragma warning restore CS8618
}
