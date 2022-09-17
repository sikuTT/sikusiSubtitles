namespace ObsWebSocket5.Message.Data.Event {
    /**
     * The audio balance value of an input has changed.

     * Event Subscription: Inputs
     */
    public class InputAudioBalanceChanged : EventData {
        /** Name of the affected input */
        string inputName;
        /** New audio balance value of the input */
        long inputAudioBalance;
    }
}
