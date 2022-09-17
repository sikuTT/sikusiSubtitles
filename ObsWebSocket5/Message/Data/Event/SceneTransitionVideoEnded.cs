namespace ObsWebSocket5.Message.Data.Event {
    /**
     * A scene transition's video has completed fully.
     * 
     * Useful for stinger transitions to tell when the video *actually* ends.
     * `SceneTransitionEnded` only signifies the cut point, not the completion of transition playback.
     * 
     * Note: Appears to be called by every transition, regardless of relevance.

     * Event Subscription: Transitions
     */
    public class SceneTransitionVideoEnded : EventData {
        /** Scene transition name */
        string transitionName;
    }
}
