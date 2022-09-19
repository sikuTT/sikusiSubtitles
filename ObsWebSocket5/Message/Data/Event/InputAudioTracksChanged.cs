namespace ObsWebSocket5.Message.Data.Event {
#pragma warning disable CS8618
    /**
     * The audio tracks of an input have changed.

     * Event Subscription: Inputs
     */
    public class InputAudioTracksChanged : EventData {
        /** Name of the input */
        public string inputName;
        /** Object of audio tracks along with their associated enable states */
        public object inputAudioTracks;
    }
#pragma warning restore CS8618
}
