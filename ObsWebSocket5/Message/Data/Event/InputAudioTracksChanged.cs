namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The audio tracks of an input have changed.

     * Event Subscription: Inputs
     */
    public class InputAudioTracksChanged : EventData {
        /** Name of the input */
        string inputName;
        /** Object of audio tracks along with their associated enable states */
        object inputAudioTracks;
    }
}
