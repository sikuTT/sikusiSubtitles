namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetCurrentSceneTransitionResponse : ResponseData {
        /** Name of the transition */
        public string transitionName;
        /** Kind of the transition */
        public string transitionKind;
        /** Whether the transition uses a fixed (unconfigurable) duration */
        public bool transitionFixed;
        /** Configured transition duration in milliseconds. `null` if transition is fixed */
        public long transitionDuration;
        /** Whether the transition supports being configured */
        public bool transitionConfigurable;
        /** Object of settings for the transition. `null` if transition is not configurable */
        public object transitionSettings;
    }
#pragma warning restore CS8618
}
