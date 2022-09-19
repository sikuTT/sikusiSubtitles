namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class GetSourceActiveResponse : ResponseData {
        /** Whether the source is showing in Program */
        public bool videoActive;
        /** Whether the source is showing in the UI (Preview, Projector, Properties) */
        public bool videoShowing;
    }
#pragma warning restore CS8618
}
