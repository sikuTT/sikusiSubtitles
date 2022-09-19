namespace ObsWebSocket5.Message.Data.Request {
#pragma warning disable CS8618
    public class BroadcastCustomEventRequest : RequestData {
        public override string GetRequestType() { return "BroadcastCustomEvent"; }
        public object eventData;
    }
#pragma warning restore CS8618
}
