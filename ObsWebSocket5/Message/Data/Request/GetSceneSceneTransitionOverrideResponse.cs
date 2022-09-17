namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSceneSceneTransitionOverrideResponse : ResponseData {
        /** Name of the overridden scene transition, else `null` */
        public string transitionName;
        /** Duration of the overridden scene transition, else `null` */
        public long transitionDuration;
    }
#pragma warning restore CS8618
}
