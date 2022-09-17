namespace ObsWebSocket5.Message.Data.Event {
    /**
     * An input's volume level has changed.

     * Event Subscription: Inputs
     */
    public class InputVolumeChanged : EventData {
        /** Name of the input */
        string inputName;
        /** New volume level in multimap */
        long inputVolumeMul;
        /** New volume level in dB */
        long inputVolumeDb;
    }
}
